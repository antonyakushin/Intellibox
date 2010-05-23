/*
Copyright (c) 2010 Stephen P Ward and Joseph E Feser

Permission is hereby granted, free of charge, to any person
obtaining a copy of this software and associated documentation
files (the "Software"), to deal in the Software without
restriction, including without limitation the rights to use,
copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the
Software is furnished to do so, subject to the following
conditions:

The above copyright notice and this permission notice shall be
included in all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES
OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT
HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY,
WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING
FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR
OTHER DEALINGS IN THE SOFTWARE.
*/
using System;
using System.Collections.Generic;
using System.Linq;
using FeserWard.Controls;

namespace IntelliboxL2E {

    public class LinqToEntitiesResultsProvider : IIntelliboxResultsProvider {

        //you would not do this in a real application because the connection would always be open.
        private NorthwindEntities Data = new NorthwindEntities();

        public IEnumerable<object> DoSearch(string searchTerm, int maxResults, object extraInfo) {

            //prime the suppliers
            foreach (var prod in Data.Products) {
                prod.SuppliersReference.Load();
            }

            //using (var nw = new NorthwindEntities()) {
            //this is a l2e limit for sql server compact
            //for real sql, you can just perform a where without the tolist.
            var l2eIsNotFun = Data.Products.ToList()
                .Where(p => p.Product_Name.StartsWith(searchTerm, StringComparison.OrdinalIgnoreCase)).Cast<object>();
            return l2eIsNotFun;
            //}
        }
    }
}
