//using System;

//namespace SolstarTestApp.Core.CQRS
//{
//    public static class Query
//    {
//        public static TQuery Using<TQuery>(Action<TQuery> action) where TQuery : new()
//        {
//            TQuery query = new TQuery();
//            action(query);
//            return query;
//        }

//        public static TQuery Using<TQuery>() where TQuery : new()
//        {
//            return new TQuery();
//        }
//    }
//}