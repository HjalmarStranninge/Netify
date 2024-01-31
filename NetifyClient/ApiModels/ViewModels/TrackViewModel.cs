using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetifyClient.ApiModels.ViewModels
{
    internal class TrackViewModel
    {
        public string Title { get; set; }

        public virtual ICollection<ArtistViewModel> Artists { get; set; }
    }
}
