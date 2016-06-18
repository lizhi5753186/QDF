using System.Threading.Tasks;
using Castle.DynamicProxy;
using QDF.Threading;
using QDF.Uow;

namespace QDF.Interceptions
{
    /// <summary>
    /// 工作单元拦截器
    /// </summary>
    public class UnitOfWorkInterceptor : IInterceptor
    {
        // 工作单元管理类
        private readonly IUnitOfWorkManager _unitOfWorkManager;

        public UnitOfWorkInterceptor(IUnitOfWorkManager unitOfWorkManager)
        {
            _unitOfWorkManager = unitOfWorkManager;
        }

        /// <summary>
        /// Intercepts a method.
        /// </summary>
        /// <param name="invocation">Method invocation arguments</param>
        public void Intercept(IInvocation invocation)
        {
            if (_unitOfWorkManager.Current != null)
            {
                //Continue with current uow
                invocation.Proceed();
                return;
            }

            var unitOfWorkAttr = UnitOfWorkAttribute.GetUnitOfWorkAttributeOrNull(invocation.MethodInvocationTarget);
            if (unitOfWorkAttr == null)
            {
                //No need to a uow
                invocation.Proceed();
                return;
            }

            //No current uow, run a new one
            PerformUow(invocation);
        }

        private void PerformUow(IInvocation invocation)
        {
            if (AsyncHelper.IsAsyncMethod(invocation.Method))
            {
                PerformAsyncUow(invocation);
            }
            else
            {
                PerformSyncUow(invocation);
            }
        }

        private void PerformSyncUow(IInvocation invocation)
        {
            invocation.Proceed();
            _unitOfWorkManager.Commit();
        }

        private void PerformAsyncUow(IInvocation invocation)
        {
            invocation.Proceed();

            if (invocation.Method.ReturnType == typeof(Task))
            {
                invocation.ReturnValue = InternalAsyncHelper.AwaitTaskWithPostActionAndFinally(
                    (Task)invocation.ReturnValue,
                    async () => await _unitOfWorkManager.CompleteAsync(),
                    exception => _unitOfWorkManager.Current.Dispose()
                    );
            }
            else //Task<TResult>
            {
                invocation.ReturnValue = InternalAsyncHelper.CallAwaitTaskWithPostActionAndFinallyAndGetResult(
                    invocation.Method.ReturnType.GenericTypeArguments[0],
                    invocation.ReturnValue,
                    async () => await _unitOfWorkManager.CompleteAsync(),
                    (exception) => _unitOfWorkManager.Current.Dispose()
                    );
            }
        }
    }
}