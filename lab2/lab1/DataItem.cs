using System.Numerics;

namespace lab1
{
    struct DataItem
    {
        public double x { get; set; }
        public double y { get; set; }
        public Vector2 field { get; set; }
        public DataItem(double x, double y, Vector2 field)
        {
            this.x = x;
            this.y = y;
            this.field = field;
        }
        public string ToLongString(string format = "")
        {
            return $"x = {x.ToString(format)} y = {y.ToString(format)} field = {field.ToString(format)} abs_value = {field.Length().ToString(format)}";
        }
        public override string ToString()
        {
            return $"x = {x} y = {y} field = {field}";
        }
    }

    public delegate Vector2 FdblVector2(double x, double y);
}
