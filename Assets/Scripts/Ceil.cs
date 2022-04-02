using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts
{
    internal class Ceil
    {
        private const CeilType defaultType = CeilType.Empty;

        public CeilType type { get; set; }

        public Ceil()
        {
            this.type = defaultType;
        }

        public void UpdateType(CeilType newType)
        {
            this.type = newType;
        }
    }
}
