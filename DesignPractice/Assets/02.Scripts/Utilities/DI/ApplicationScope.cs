using DP.Contexts;

namespace DP.Utilities.DI
{
    /// <summary>
    /// 어플리케이션 시작부터 끝까지 전체 범위에서 해결해야하는 의존을 관리
    /// </summary>
    public class ApplicationScope : Scope
    {
        public override void Register()
        {
            base.Register();

            Container.Register<CurrencyContext>(); // 어플리케이션 전체에 걸친 객체들에게 재화컨텍스트를 주입하겠다.
        }
    }
}