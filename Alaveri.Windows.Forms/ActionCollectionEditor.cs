using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alaveri.Windows.Forms
{
    public class ActionCollectionEditor: CollectionEditor
    {
        public ActionCollectionEditor(Type type) : base(type)
        {

        }

        protected override CollectionForm CreateCollectionForm()
        {
            var baseForm = base.CreateCollectionForm();
            baseForm.Text = "Action Collection Editor";
            return baseForm;
        }

        protected override bool CanSelectMultipleInstances()
        {
            return false;
        }

        protected override Type CreateCollectionItemType()
        {
            return typeof(Action);
        }
    }
}
