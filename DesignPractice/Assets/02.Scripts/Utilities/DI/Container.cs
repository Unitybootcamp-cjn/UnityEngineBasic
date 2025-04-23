using System;
using System.Collections.Generic;

namespace DP.Utilities.DI
{
    /// <summary>
    /// 의존성 주입시 사용할 객체들을 가지고 있음 (배달 박스 같은거임)
    /// </summary>
    public class Container
    {
        public Container()
        {
            _registration = new Dictionary<Type, object>();
        }


        Dictionary<Type, object> _registration;

        /// <summary>
        /// 사용할 객체 등록
        /// </summary>
        /// <typeparam name="T"> 사용할 객체 타입 </typeparam>
        public void Register<T>()
            where T : class, new() // T 타입은 클래스 타입이면서 new 키워드로 생성할 수 있다.
        {
            T obj = new T();
            _registration[typeof(T)] = obj;
        }

        /// <summary>
        /// 등록된 객체 탐색하여 받아옴
        /// </summary>
        /// <typeparam name="T"> 가져오고 싶은 객체 타입 </typeparam>
        public T Resolve<T>()
        {
            return (T)  _registration[typeof(T)];
        }

        public object Resolve(Type type)
        {
            return _registration[type];
        }
    }
}