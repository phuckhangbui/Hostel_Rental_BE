﻿namespace BusinessObject
{
    public class ContractDetail
    {
        public int ContractDetailID { get; set; }
        public Contract Contract { get; set; }
        public Service Service { get; set; }
    }
}
