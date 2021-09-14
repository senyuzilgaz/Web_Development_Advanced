namespace RestApi
{
    
    //Simple object which contains Currency, amount, and Operation
    public class Object
    {

        public string getOper(){return this.oper;}
        public void setOper(string oper){this.oper = oper;}

        public string getCurrency(){return this.currency;}
        public void setCurrency(string currency){this.currency = currency;}

        public int getAmount(){return this.amount;}
        public void setAmount(int amount){this.amount = amount;}

        public string currency;
        public int amount;
        public string oper;
    }
}