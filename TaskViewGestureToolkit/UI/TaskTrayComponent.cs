using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskViewGestureToolkit.UI
{
    public partial class TaskTrayComponent : Component
    {
        public TaskTrayComponent()
        {
            InitializeComponent();
        }

        public TaskTrayComponent(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }
    }
}
