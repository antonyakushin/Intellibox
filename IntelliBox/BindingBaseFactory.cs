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
using System.Windows.Data;

namespace System.Windows.Controls {
    internal static class BindingBaseFactory {

        private static BindingBase ConstructBinding(Intellibox source, BindingBase template, string propertyName) {
            if (source == null)
                throw new ArgumentNullException("source");

            if (string.IsNullOrEmpty(propertyName))
                throw new ArgumentOutOfRangeException("propertyName", "Cannot be null or empty.");

            if (template == null)
                return new Binding(propertyName) {
                    Source = source
                };

            if (!(template is Binding) && !(template is MultiBinding) && !(template is PriorityBinding))
                throw new ArgumentOutOfRangeException("template", "Unrecognized BindingBase: " + template.GetType().ToString());


            var bind = CloneHelper.Clone(template);
            if (bind is Binding) {
                ValidateAndSetSource(source, bind as Binding, propertyName);
            }
            else if (bind is MultiBinding) {
                var a = bind as MultiBinding;
                // all of these binding have already been cloned, so its safe to set them back to themselves
                for (int i = 0; i < a.Bindings.Count; i++) {
                    a.Bindings[i] = ConstructBinding(source, a.Bindings[i], propertyName);
                }
            }
            else if (bind is PriorityBinding) {
                var a = bind as PriorityBinding;
                // all of these binding have already been cloned, so its safe to set them back to themselves
                for (int i = 0; i < a.Bindings.Count; i++) {
                    a.Bindings[i] = ConstructBinding(source, a.Bindings[i], propertyName);
                }
            }
            
            return bind;
        }

        private static void ValidateAndSetSource(Intellibox source, Binding bind, string propertyName) {
            if (!string.IsNullOrEmpty(bind.ElementName))
                throw new ArgumentOutOfRangeException("The IntelliBox control does not support setting 'ElementName' on the DisplayValueBinding or SelectedValueBinding properties.");

            if (bind.RelativeSource != null)
                throw new ArgumentOutOfRangeException("The IntelliBox control does not support setting 'RelativeSource' on the DisplayValueBinding or SelectedValueBinding properties.");

            if (bind.XPath != null)
                throw new ArgumentOutOfRangeException("The IntelliBox control does not support setting XPath binding expressions.");

            string path = "." + bind.Path.Path ?? string.Empty;

            bind.Path = new PropertyPath(propertyName + path, bind.Path.PathParameters);
            bind.Source = source;
        }

        public static BindingBase ConstructBindingForHighlighted(Intellibox source, BindingBase template) {
            return ConstructBinding(source, template, "ResultsList.SelectedItem");
        }

        public static BindingBase ConstructBindingForSelected(Intellibox source, BindingBase template) {
            return ConstructBinding(source, template, "SelectedItem");
        }
    }
}
