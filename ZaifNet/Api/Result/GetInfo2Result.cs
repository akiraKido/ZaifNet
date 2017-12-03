using System;
using static System.Environment;
using ZaifNet.Common;

namespace ZaifNet.Api.Result
{
    public class GetInfo2Result : IJsonApplyable
    {
        public Funds Funds { get; private set; }
        public Funds Deposit { get; private set; }
        public Rights Rights { get; private set; }
        public int OpenOrders { get; private set; }
        public DateTime ServerTime { get; private set; }

        private bool _isInitialized;

        public void ApplyJson(dynamic json)
        {
            if(_isInitialized) throw new InvalidOperationException("cannot call ApplyJson twice");
            
            Funds = new Funds(json.funds);
            Deposit = new Funds(json.deposit);
            Rights = new Rights(json.rights);
            OpenOrders = json.open_orders;
            ServerTime = ((long) json.server_time).ToDateTime();

            _isInitialized = true;
        }

        public override string ToString()
        {
            return $"Funds: {Funds}{NewLine}" +
                   $"Deposit: {Deposit}{NewLine}" +
                   $"Rights: {Rights}{NewLine}" +
                   $"OpenOrders: {OpenOrders}{NewLine}" +
                   $"ServerTime: {ServerTime}{NewLine}";
        }
    }

    public class Rights
    {
        public bool Info { get; }
        public bool Trade { get; }
        public bool Withdraw { get; }
        public bool PersonalInfo { get; }

        internal Rights(dynamic json)
        {
            Info = json.info == 1;
            Trade = json.trade == 1;
            Withdraw = json.withdraw == 1;
            PersonalInfo = json.personal_info == 1;
        }

        public override string ToString()
        {
            return $"Info: {Info}{NewLine}" +
                   $"Trade: {Trade}{NewLine}" +
                   $"Withdraw: {Withdraw}{NewLine}" +
                   $"PersonalInfo: {PersonalInfo}{NewLine}";
        }
    }
}