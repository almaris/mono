//
// System.Configuration.TypeNameConverterTest.cs - Unit tests
// for System.Configuration.TypeNameConverter.
//
// Author:
//	Chris Toshok  <toshok@ximian.com>
//
// Copyright (C) 2005 Novell, Inc (http://www.novell.com)
//
// Permission is hereby granted, free of charge, to any person obtaining
// a copy of this software and associated documentation files (the
// "Software"), to deal in the Software without restriction, including
// without limitation the rights to use, copy, modify, merge, publish,
// distribute, sublicense, and/or sell copies of the Software, and to
// permit persons to whom the Software is furnished to do so, subject to
// the following conditions:
// 
// The above copyright notice and this permission notice shall be
// included in all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
// EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
// MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
// NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE
// LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION
// OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION
// WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
//


using System;
using System.Configuration;
using NUnit.Framework;

namespace MonoTests.System.Configuration {
	[TestFixture]
	public class TypeNameConverterTest
	{
		[Test]
		public void CanConvertFrom ()
		{
			TypeNameConverter cv = new TypeNameConverter ();

			Assert.IsTrue (cv.CanConvertFrom (null, typeof (string)), "A1");
			Assert.IsFalse (cv.CanConvertFrom (null, typeof (TimeSpan)), "A2");
			Assert.IsFalse (cv.CanConvertFrom (null, typeof (int)), "A3");
			Assert.IsFalse (cv.CanConvertFrom (null, typeof (object)), "A4");
		}

		[Test]
		public void CanConvertTo ()
		{
			TypeNameConverter cv = new TypeNameConverter ();

			Assert.IsTrue (cv.CanConvertTo (null, typeof (string)), "A1");
			Assert.IsFalse (cv.CanConvertTo (null, typeof (TimeSpan)), "A2");
			Assert.IsFalse (cv.CanConvertTo (null, typeof (int)), "A3");
			Assert.IsFalse (cv.CanConvertTo (null, typeof (object)), "A4");
		}

		[Test]
		public void ConvertFrom ()
		{
			TypeNameConverter cv = new TypeNameConverter ();
			object t;

			t = cv.ConvertFrom (null, null, typeof (object).AssemblyQualifiedName);
			Assert.IsTrue (t is Type, "A1");
			object o = Activator.CreateInstance ((Type)t);
			Assert.AreEqual (typeof (object), o.GetType(), "A2");
		}

		[Test]
		[ExpectedException (typeof (InvalidCastException))]
		public void ConvertFrom_TypeError ()
		{
			TypeNameConverter cv = new TypeNameConverter ();
			object o;

			o = cv.ConvertFrom (null, null, 59);
			Assert.IsNull (o, "A1");
		}

		[Test]
		public void ConvertTo ()
		{
			TypeNameConverter cv = new TypeNameConverter ();

			Assert.AreEqual (typeof (string).AssemblyQualifiedName, cv.ConvertTo (null, null, typeof (string), typeof (string)), "A1");
		}

		[Test]
		public void ConvertTo_NullError ()
		{
			TypeNameConverter cv = new TypeNameConverter ();

			Assert.AreEqual (null, cv.ConvertTo (null, null, null, typeof (string)), "A1");
		}

		[Test]
		[ExpectedException (typeof (ArgumentException))]
		public void ConvertTo_TypeError1 ()
		{
			TypeNameConverter cv = new TypeNameConverter ();

			Assert.AreEqual ("59", cv.ConvertTo (null, null, 59, typeof (string)), "A1");
		}
	}
}

