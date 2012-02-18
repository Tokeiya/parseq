﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Parseq.Combinators
{
    public static class Prims{

        public static Parser<TToken, Unit> Ignore<TToken,TResult>(
            this Parser<TToken,TResult> parser)
        {
            if (parser == null)
                throw new ArgumentNullException("parser");

            return parser.Select(_ => Unit.Instance);
        }

        public static Parser<TToken, TToken> Satisfy<TToken>(
            Func<TToken, bool> predicate)
        {
            if (predicate == null)
                throw new ArgumentNullException("selector");
            return stream => {
                TToken value;
                return stream.TryGetValue(out value) && predicate(value) ?
                    Reply.Success<TToken, TToken>(stream.Next(), value) :
                    Reply.Failure<TToken, TToken>(stream);
            };
        }

        public static Parser<TToken, TToken> Any<TToken>(){
            return Prims.Satisfy<TToken>(_ => true);
        }

        public static Parser<TToken, TToken> OneOf<TToken>(
            Func<TToken, TToken, bool> predicate, params TToken[] candidates){
            if (predicate == null)
                throw new ArgumentNullException("predicate");
            if (candidates == null)
                throw new ArgumentNullException("candidates");

            return Combinator.Choice(candidates.Select(x =>
                Prims.Satisfy<TToken>(y =>
                    predicate(x, y))).ToArray());
        }

        public static Parser<TToken, TToken> OneOf<TToken>(
            params TToken[] candidates)
        {
            return Prims.OneOf<TToken>(
                EqualityComparer<TToken>.Default.Equals, candidates);
        }

        public static Parser<TToken, TToken> NoneOf<TToken>(
            Func<TToken, TToken, bool> predicate, params TToken[] candidates)
        {
            if (predicate == null)
                throw new ArgumentNullException("predicate");
            if (candidates == null)
                throw new ArgumentNullException("candidates");

            return Combinator.Choice(candidates.Select(x =>
                Prims.Satisfy<TToken>(y =>
                    !predicate(x, y))).ToArray());
        }

        public static Parser<TToken, TToken> NoneOf<TToken>(
            params TToken[] candidates)
        {
            return Prims.NoneOf<TToken>(
                EqualityComparer<TToken>.Default.Equals, candidates);
        }

        public static Parser<TToken, TResult2> Pipe<TToken, TResult0, TResult1, TResult2>(
            this Parser<TToken, TResult0> parser0,
            Parser<TToken, TResult1> parser1,
            Func<TResult0, TResult1, TResult2> selector)
        {
            if (parser0 == null)
                throw new ArgumentNullException("parser0");
            if (parser1 == null)
                throw new ArgumentNullException("parser1");
            if (selector == null)
                throw new ArgumentNullException("selector");

            return from x in parser0
                   from y in parser1
                   select selector(x, y);
        }

        public static Parser<TToken, TResult3> Pipe<TToken, TResult0, TResult1, TResult2, TResult3>(
            this Parser<TToken, TResult0> parser0,
            Parser<TToken, TResult1> parser1,
            Parser<TToken, TResult2> parser2,
            Func<TResult0, TResult1, TResult2, TResult3> selector)
        {
            if (parser0 == null)
                throw new ArgumentNullException("parser0");
            if (parser1 == null)
                throw new ArgumentNullException("parser1");
            if (parser2 == null)
                throw new ArgumentNullException("parser2");
            if (selector == null)
                throw new ArgumentNullException("selector");

            return from x in parser0
                   from y in parser1
                   from z in parser2
                   select selector(x, y, z);
        }

        public static Parser<TToken, TResult4> Pipe<TToken, TResult0, TResult1, TResult2, TResult3, TResult4>(
            this Parser<TToken, TResult0> parser0,
            Parser<TToken, TResult1> parser1,
            Parser<TToken, TResult2> parser2,
            Parser<TToken, TResult3> parser3,
            Func<TResult0, TResult1, TResult2, TResult3, TResult4> selector)
        {
            if (parser0 == null)
                throw new ArgumentNullException("parser0");
            if (parser1 == null)
                throw new ArgumentNullException("parser1");
            if (parser2 == null)
                throw new ArgumentNullException("parser2");
            if (parser3 == null)
                throw new ArgumentNullException("parser3");
            if (selector == null)
                throw new ArgumentNullException("selector");

            return from x in parser0
                   from y in parser1
                   from z in parser2
                   from a in parser3
                   select selector(x, y, z, a);
        }

        public static Parser<TToken, TResult5> Pipe<TToken, TResult0, TResult1, TResult2, TResult3, TResult4, TResult5>(
            this Parser<TToken, TResult0> parser0,
            Parser<TToken, TResult1> parser1,
            Parser<TToken, TResult2> parser2,
            Parser<TToken, TResult3> parser3,
            Parser<TToken, TResult4> parser4,
            Func<TResult0, TResult1, TResult2, TResult3, TResult4, TResult5> selector)
        {
            if (parser0 == null)
                throw new ArgumentNullException("parser0");
            if (parser1 == null)
                throw new ArgumentNullException("parser1");
            if (parser2 == null)
                throw new ArgumentNullException("parser2");
            if (parser3 == null)
                throw new ArgumentNullException("parser3");
            if (parser4 == null)
                throw new ArgumentNullException("parser4");
            if (selector == null)
                throw new ArgumentNullException("selector");

            return from x in parser0
                   from y in parser1
                   from z in parser2
                   from a in parser3
                   from b in parser4
                   select selector(x, y, z, a, b);
        }

        public static Parser<TToken, TResult0> Left<TToken, TResult0, TResult1>(
            this Parser<TToken, TResult0> parser0,
            Parser<TToken, TResult1> parser1)
        {
            if (parser0 == null)
                throw new ArgumentNullException("parser0");
            if (parser1 == null)
                throw new ArgumentNullException("parser1");

            return from x in parser0
                   from _ in parser1
                   select x;
        }

        public static Parser<TToken, TResult1> Right<TToken, TResult0, TResult1>(
            this Parser<TToken, TResult0> parser0,
            Parser<TToken, TResult1> parser1)
        {
            if (parser0 == null)
                throw new ArgumentNullException("parser0");
            if (parser1 == null)
                throw new ArgumentNullException("parser1");

            return from _ in parser0
                   from y in parser1
                   select y;
        }

        public static Parser<TToken, Tuple<TResult0, TResult1>> Both<TToken, TResult0, TResult1>(
            this Parser<TToken, TResult0> parser0,
            Parser<TToken, TResult1> parser1)
        {
            if (parser0 == null)
                throw new ArgumentNullException("parser0");
            if (parser1 == null)
                throw new ArgumentNullException("parser1");

            return from x in parser0
                   from y in parser1
                   select Tuple.Create(x, y);
        }

        public static Parser<TToken, TResult0> Between<TToken, TResult0, TResult1, TResult2>(
            this Parser<TToken, TResult0> parser0,
            Parser<TToken, TResult1> parser1,
            Parser<TToken, TResult2> parser2)
        {
            if (parser0 == null)
                throw new ArgumentNullException("parser0");
            if (parser1 == null)
                throw new ArgumentNullException("parser1");
            if (parser2 == null)
                throw new ArgumentNullException("parser2");

            return from x in parser1
                   from y in parser0
                   from z in parser2
                   select y;
        }

        public static Parser<TToken, TResult[]> SepBy<TToken, TResult, TSeparator>(
            this Parser<TToken, TResult> parser,
            int count,
            Parser<TToken, TSeparator> separator)
        {
            if (parser == null)
                throw new ArgumentNullException("parser");
            if (count < 0)
                throw new ArgumentOutOfRangeException("count");
            if (separator == null)
                throw new ArgumentNullException("separator");

            return from x in parser.Many(count)
                   from y in separator.Right(parser).Many()
                   select Enumerable.Concat(x, y).ToArray();
        }

        public static Parser<TToken, TResult[]> SepBy<TToken, TResult, TSeparator>(
            this Parser<TToken, TResult> parser,
            Parser<TToken, TSeparator> separator)
        {
            return parser.SepBy(0, separator);
        }

        public static Parser<TToken, TResult[]> SepEndBy<TToken, TResult, TSeparator>(
            this Parser<TToken, TResult> parser,
            int count,
            Parser<TToken, TSeparator> separator)
        {
            if (parser == null)
                throw new ArgumentNullException("parser");
            if (count < 0)
                throw new ArgumentOutOfRangeException("count");
            if (separator == null)
                throw new ArgumentNullException("separator");

            return from x in parser.Many(count)
                   from y in separator.Right(parser).Many()
                   from z in separator.Maybe()
                   select Enumerable.Concat(x, y).ToArray();
        }

        public static Parser<TToken, TResult[]> SepEndBy<TToken, TResult, TSeparator>(
            this Parser<TToken, TResult> parser,
            Parser<TToken, TSeparator> separator)
        {
            return parser.SepEndBy(0, separator);
        }

        public static Parser<TToken, TResult[]> While<TToken,T, TResult>(
            this Parser<TToken, TResult> parser, Parser<TToken, T> condition)
        {
            if (parser == null)
                throw new ArgumentNullException("parser");
            if (condition == null)
                throw new ArgumentNullException("condition");

            return (condition.Not().Right(parser)).Many();
        }

        public static Parser<TToken, TResult[]> DoWhile<TToken,T, TResult>(
            this Parser<TToken, TResult> parser, Parser<TToken,T> condition)
        {
            if (parser == null)
                throw new ArgumentNullException("parser");
            if (condition == null)
                throw new ArgumentNullException("condition");

            return from x in parser
                   from y in parser.While(condition)
                   select (new[] { x }).Concat(y).ToArray();
        }
    }
}