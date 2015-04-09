using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PassManNG
{
    public class ProfileClass
    {
        private Items currentProfile;

        // Property
        public Items CurrentProfile
        {
            get
            {
                return currentProfile;
            }
            set
            {
                currentProfile = value;
            }
        }
    }
}
