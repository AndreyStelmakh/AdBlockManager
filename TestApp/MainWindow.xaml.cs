using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using static TestApp.MainWindow;

namespace TestApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ObservableCollection<TimeBlock> _timeBlocks;

        public MainWindow()
        {

            InitializeComponent();

            DataContext = this;

            Random r = new Random();
            int i = 0;
            var q = from k in new object[100] select new TimeBlock() { LengthSeconds = 20 * r.Next(4, 14), Type = ++ i % 3 == 0 ? TimeBlock.Types.Empty :  i%3 == 1 ? TimeBlock.Types.R1 : TimeBlock.Types.R2 };
            _timeBlocks = new ObservableCollection<TimeBlock>(q);

        }

        public ObservableCollection<TimeBlock> TimeBlocks
        {
            get
            {
                return _timeBlocks;
            }

            set
            {
                _timeBlocks = value;
            }
        }

        public class TimeBlock
        {
            private DateTime _dateFrom;
            private DateTime _dateTo;
            private Types _type;

            public DateTime DateFrom
            {
                get
                {
                    return _dateFrom;
                }

                set
                {
                    _dateFrom = value;
                }
            }

            public DateTime DateTo
            {
                get
                {
                    return _dateTo;
                }

                set
                {
                    _dateTo = value;
                }
            }

            private int lengthSeconds;

            public string LengthFormatted
            {
                get
                {
                    return "0:02:00.0";
                }
            }

            public int LengthSeconds
            {
                get
                {
                    return lengthSeconds;
                }

                set
                {
                    lengthSeconds = value;
                }
            }

            public Types Type
            {
                get
                {
                    return _type;
                }

                set
                {
                    _type = value;
                }
            }

            public enum Types { Regular, Empty, R1, R2 }
        }

    }
    public class TimeBlockDataTemplateSelector : DataTemplateSelector
    {

        public DataTemplate RegularBlockTemplate
        {
            get
            {
                return _regularBlockTemplate;
            }

            set
            {
                _regularBlockTemplate = value;
            }
        }

        public DataTemplate EmptyBlockTemplate
        {
            get
            {
                return _emptyBlockTemplate;
            }

            set
            {
                _emptyBlockTemplate = value;
            }
        }

        public DataTemplate RegularBlockTemplate1
        {
            get
            {
                return _regularBlockTemplate1;
            }

            set
            {
                _regularBlockTemplate1 = value;
            }
        }

        public DataTemplate RegularBlockTemplate2
        {
            get
            {
                return _regularBlockTemplate2;
            }

            set
            {
                _regularBlockTemplate2 = value;
            }
        }

        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            var dataItem = item as TimeBlock;

            if (dataItem != null)
            {
                switch (dataItem.Type)
                {
                    case TimeBlock.Types.Regular:
                        return RegularBlockTemplate;
                    case TimeBlock.Types.Empty:
                        return EmptyBlockTemplate;
                    case TimeBlock.Types.R1:
                        return RegularBlockTemplate1;

                    case TimeBlock.Types.R2:
                        return RegularBlockTemplate2;
                }
            }

            return base.SelectTemplate(item, container);
        }

        private DataTemplate _regularBlockTemplate;
        private DataTemplate _regularBlockTemplate1;
        private DataTemplate _regularBlockTemplate2;
        private DataTemplate _emptyBlockTemplate;
    }
}
