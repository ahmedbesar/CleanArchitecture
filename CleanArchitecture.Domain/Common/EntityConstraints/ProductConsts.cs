using System;
using System.Collections.Generic;
using System.Text;

namespace CleanArchitecture.Domain.Common.EntityConstraints
{
    public static class ProductConsts
    {
        public const int ProductNameMaxLength = 200;
        public const int ProductDescriptionMaxLength = 1000;
        public const string ProductPriceColumnType = "decimal(18,2)";
    }
}
