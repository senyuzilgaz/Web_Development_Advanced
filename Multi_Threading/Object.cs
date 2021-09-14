namespace RestApi
{
    
    //Simple object which contains Name, is Complete, and Operation
    public class Object
    {
        /*
        public Object(){
            name = "Not_Initialized";
            isComplete = false;
            oper = "Not_Initialized";
        }
        public Object(string name, bool isComplete, string oper){
            this.name = name;
            this.isComplete = isComplete;
            this.oper = oper;
        }
        */
        public string getOper(){return this.oper;}
        public void setOper(string oper){this.oper = oper;}

        public string getName(){return this.name;}
        public void setName(string name){this.name = name;}

        public bool getComplete(){return this.isComplete;}
        public void setComplete(bool isComplete){this.isComplete = isComplete;}

        public string name;
        public bool isComplete;
        public string oper;
    }
}