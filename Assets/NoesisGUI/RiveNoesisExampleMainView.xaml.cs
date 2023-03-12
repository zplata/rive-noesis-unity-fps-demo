#if UNITY_5_3_OR_NEWER
#define NOESIS
using Noesis;
#else
using System;
using System.Windows.Controls;
#endif

namespace RiveNoesisExample
{
    /// <summary>
    /// Interaction logic for RiveNoesisExampleMainView.xaml
    /// </summary>
    public partial class RiveNoesisExampleMainView : UserControl
    {
        public RiveNoesisExampleMainView()
        {
            InitializeComponent();
        }

#if NOESIS
        private void InitializeComponent()
        {
            NoesisUnity.LoadComponent(this);
        }
#endif
    }
}
