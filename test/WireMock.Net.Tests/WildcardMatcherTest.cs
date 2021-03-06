﻿using NFluent;
using Xunit;
using WireMock.Matchers;

namespace WireMock.Net.Tests
{
    //[TestFixture]
    public class WildcardMatcherTest
    {
        [Fact]
        public void WildcardMatcher_patterns_positive()
        {
            var tests = new[]
            {
                new { p = "*", i = "" },
                new { p = "?", i = " " },
                new { p = "*", i = "a" },
                new { p = "*", i = "ab" },
                new { p = "?", i = "a" },
                new { p = "*?", i = "abc" },
                new { p = "?*", i = "abc" },
                new { p = "abc", i = "abc" },
                new { p = "abc*", i = "abc" },
                new { p = "abc*", i = "abcd" },
                new { p = "*abc*", i = "abc" },
                new { p = "*a*bc*", i = "abc" },
                new { p = "*a*b?", i = "aXXXbc" }
            };

            foreach (var test in tests)
            {
                var matcher = new WildcardMatcher(test.p);
                Check.That(matcher.IsMatch(test.i)).Equals(1.0);
                //Assert.AreEqual(1.0, matcher.IsMatch(test.i), "p = " + test.p + ", i = " + test.i);
            }
        }

        [Fact]
        public void WildcardMatcher_patterns_negative()
        {
            var tests = new[]
            {
                new { p = "*a", i = ""},
                new { p = "a*", i = ""},
                new { p = "?", i = ""},
                new { p = "*b*", i = "a"},
                new { p = "b*a", i = "ab"},
                new { p = "??", i = "a"},
                new { p = "*?", i = ""},
                new { p = "??*", i = "a"},
                new { p = "*abc", i = "abX"},
                new { p = "*abc*", i = "Xbc"},
                new { p = "*a*bc*", i = "ac"}
            };

            foreach (var test in tests)
            {
                var matcher = new WildcardMatcher(test.p);
                //Assert.AreEqual(0.0, matcher.IsMatch(test.i), "p = " + test.p + ", i = " + test.i);
                Check.That(matcher.IsMatch(test.i)).Equals(0.0);
            }
        }
    }
}