using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Data;

namespace System.Windows.Controls.Custom {
    internal static class BindingBaseFactory {

        private static BindingBase ConstructBinding(IntelliBox source, BindingBase template, string propertyName) {
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

        private static void ValidateAndSetSource(IntelliBox source, Binding bind, string propertyName) {
            if (!string.IsNullOrEmpty(bind.ElementName))
                throw new ArgumentOutOfRangeException("The IntelliBox control does not support setting 'ElementName' on the DisplayValueBinding or SelectedValueBinding properties.");

            if (bind.RelativeSource != null)
                throw new ArgumentOutOfRangeException("The IntelliBox control does not support setting 'RelativeSource' on the DisplayValueBinding or SelectedValueBinding properties.");

            string path = "." + bind.Path.Path ?? string.Empty;

            bind.Path = new PropertyPath(propertyName + path, bind.Path.PathParameters);
            bind.Source = source;
        }

        public static BindingBase ConstructBindingForHighlighted(IntelliBox source, BindingBase template) {
            return ConstructBinding(source, template, "HighlightedItem");
        }

        public static BindingBase ConstructBindingForSelected(IntelliBox source, BindingBase template) {
            return ConstructBinding(source, template, "SelectedItem");
        }
    }
}
