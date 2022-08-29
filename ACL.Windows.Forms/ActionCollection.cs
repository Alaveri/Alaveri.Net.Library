using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Drawing.Design;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ACL.Windows.Forms
{
    [Editor(typeof(ActionCollectionEditor), typeof(UITypeEditor))]
    public class ActionCollection : Collection<Action>, IList, IList<Action>
    { 
        public new void Add(Action action)
        {
            Items.Add(action);
        }

        public void AddRange(IEnumerable<Action> collection)
        {
            foreach (var action in collection)
                Items.Add(action);
        }

        public new Action this[int index] { get => Items[index]; set => Items[index] = value; }
    }
}
