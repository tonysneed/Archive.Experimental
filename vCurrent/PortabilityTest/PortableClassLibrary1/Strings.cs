using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;
#if NET45
    using System.ComponentModel.DataAnnotations.Schema;
#endif

namespace PortableClassLibrary1
{
    public class Strings
    {
#if NET45
        [NotMapped]
#endif
        public ObservableCollection<string> Items { get; set; }

#if !SILVERLIGHT
        public void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            
        }
#endif
    }
}
