using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

public class Inventory_UI : Form
{
    private Button PriceList = new Button();
    private Button ChangeData = new Button();
    private Button create_File = new Button();
    private Button Clear = new Button();
    private Button Exit = new Button();
    private Button SoldUpdate = new Button();
    private CheckBox remove = new CheckBox();
    private RadioButton Total = new RadioButton();
    private RadioButton SendAmazon = new RadioButton();
    private RadioButton View = new RadioButton();
    private RadioButton total_Sold = new RadioButton();
    private BSTree inventory = new BSTree();
    private Label Sku_Label = new Label();
    private Label change_Label = new Label();
    private Label output = new Label();
    private TextBox SKU_Entry = new TextBox();
    private TextBox Change_Total = new TextBox();
    private Font bold15 = new Font("Arial", 10, FontStyle.Bold);


    public Inventory_UI()
    {

        //Initialize text
        Text = "Inventory Shit";
        PriceList.Text = "Update Price List";
        Sku_Label.Text = "Enter SKU";
        SKU_Entry.Text = "";
        Total.Text = "Add to Total";
        SendAmazon.Text = "Sent to Amazon";
        change_Label.Text = "Quantity Changed";
        ChangeData.Text = "Update Inventory";
        create_File.Text = "Send Shit to Steve";
        output.Text = "";
        Clear.Text = "Clear";
        Exit.Text = "Exit";
        View.Text = "View SKU";
        remove.Text = "Remove";
        total_Sold.Text = "Add to Sold";
        SoldUpdate.Text = "Update Sold";


        //Set Sizes
        Size = new Size(800, 500);
        PriceList.Size = new Size(120, 25);
        SoldUpdate.Size = new Size(120, 25);
        SKU_Entry.Size = new Size(100, 70);
        Sku_Label.Size = new Size(100, 20);
        ChangeData.Size = new Size(100, 70);
        ChangeData.AutoSize = true;
        create_File.Size = new Size(100, 70);
        create_File.AutoSize = true;
        Total.Size = new Size(50, 50);
        Total.AutoSize = true;
        SendAmazon.Size = new Size(50, 50);
        SendAmazon.AutoSize = true;
        change_Label.Size = new Size(100, 20);
        change_Label.AutoSize = true;
        Change_Total.Size = new Size(100, 70);
        output.AutoSize = true;
        Clear.Size = new Size(100, 50);
        Exit.Size = new Size(100, 50);
        View.Size = new Size(50, 50);
        View.AutoSize = true;
        remove.Size = new Size(50, 50);
        remove.AutoSize = true;
        total_Sold.Size = new Size(50, 50);
        total_Sold.AutoSize = true;

        //Set Locations
        PriceList.Location = new Point(650, 40);
        SoldUpdate.Location = new Point(650, PriceList.Bottom + 15);
        SKU_Entry.Location = new Point(100, 100);
        Sku_Label.Location = new Point(100, SKU_Entry.Top - 20);
        View.Location = new Point(100, SKU_Entry.Bottom + 40);
        Total.Location = new Point(View.Right + 50, SKU_Entry.Bottom + 40);
        Change_Total.Location = new Point(SKU_Entry.Right + 100, 100);
        change_Label.Location = new Point(Change_Total.Left, Change_Total.Top - 20);
        SendAmazon.Location = new Point(Change_Total.Left + 15, SKU_Entry.Bottom + 40);
        ChangeData.Location = new Point(100, 350);
        create_File.Location = new Point(Change_Total.Left, 350);
        output.Location = new Point(100, 190);
        Clear.Location = new Point(600, 300);
        Exit.Location = new Point(600, Clear.Bottom + 15);
        remove.Location = new Point(Change_Total.Right + 50, SKU_Entry.Top);
        total_Sold.Location = new Point(SendAmazon.Right + 90, SKU_Entry.Bottom + 40);


        //Add Controls
        Controls.Add(PriceList);
        Controls.Add(SKU_Entry);
        Controls.Add(Sku_Label);
        Controls.Add(Total);
        Controls.Add(SendAmazon);
        Controls.Add(Change_Total);
        Controls.Add(change_Label);
        Controls.Add(ChangeData);
        Controls.Add(create_File);
        Controls.Add(output);
        Controls.Add(Clear);
        Controls.Add(Exit);
        Controls.Add(View);
        Controls.Add(remove);
        Controls.Add(total_Sold);
        Controls.Add(SoldUpdate);

        //Event Handlers
        PriceList.Click += new EventHandler(add_Click);
        SoldUpdate.Click += new EventHandler(Sold_Click);
        ChangeData.Click += new EventHandler(Update_Click);
        Clear.Click += new EventHandler(Clear_Click);
        Exit.Click += new EventHandler(Exit_Click);
        create_File.Click += new EventHandler(Create_Click);

        //Additional Features
        Sku_Label.Font = bold15;
        SendAmazon.Font = bold15;
        Total.Font = bold15;
        change_Label.Font = bold15;
        ChangeData.Font = bold15;
        create_File.Font = bold15;
        output.Font = bold15;
        create_File.BackColor = Color.LightCyan;
        ChangeData.BackColor = Color.LightCyan;
        Clear.BackColor = Color.LightCyan;
        Exit.BackColor = Color.LightCyan;
        Clear.Font = bold15;
        Exit.Font = bold15;
        View.Font = bold15;
        remove.Font = bold15;
        total_Sold.Font = bold15;
        AcceptButton = ChangeData;
        View.Checked = true;
        CenterToScreen();
        ActiveControl = SKU_Entry;

        //Set tabs
        SKU_Entry.TabIndex = 0;
        Change_Total.TabIndex = 1;

        //Load data
        LoadData();
    }
    protected override void Dispose(bool disposing)
    {
        inventory.InOrderPrint();
        base.Dispose(disposing);
    }

    #region Txt File Generators And Readers
    protected void Sold_Click(Object sender, EventArgs e)
    {
        OpenFileDialog o = new OpenFileDialog();
        o.Filter = "Text Files (.txt)|*.txt|All Files (*.*)|*.*";
        o.FilterIndex = 1;
        ID_Tree LoadData = new ID_Tree();

        string path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\DataJoe";
        StreamReader load = new StreamReader(path + "\\IdStore.txt");
        string soldLine;

        while ((soldLine = load.ReadLine()) != null)
        {
            string[] data = soldLine.Split(new char[] { '\t' }, StringSplitOptions.RemoveEmptyEntries);
            if (data.Length == 1)
            {
                LoadData.insert(data[0]);
            }
            else
            {
                MessageBox.Show("Debug this file Joseph");
            }
        }

        load.Close();

        if (o.ShowDialog() == DialogResult.OK)
        {
            StreamReader read = new StreamReader(o.OpenFile());
            string line;
            string bollox = read.ReadLine();
            while ((line = read.ReadLine()) != null)
            {
                string[] data = line.Split(new char[] { '\t' }, StringSplitOptions.RemoveEmptyEntries);

                if (data.Length == 20)
                {
                    if (!LoadData.search(data[1]))
                    {
                        inventory.AddSold(data[2], int.Parse(data[6]), false);
                        inventory.AddAmazon(data[2], int.Parse(data[6]));
                        LoadData.insert(data[1]);
                    }
                }
            }
            read.Close();
        }

        LoadData.PreOrderPrint();
    }

    protected void add_Click(Object sender, EventArgs e)
    {
        OpenFileDialog o = new OpenFileDialog();
        o.Filter = "Text Files (.txt)|*.txt|All Files (*.*)|*.*";
        o.FilterIndex = 1;

        if (o.ShowDialog() == DialogResult.OK)
        {
            StreamReader read = new StreamReader(o.OpenFile());
            string line;
            string bollox = read.ReadLine();
            while ((line = read.ReadLine()) != null)
            {
                string[] data = line.Split(new char[] { '\t' }, StringSplitOptions.RemoveEmptyEntries);
                if (data.Length == 3)
                {
                    inventory.insert(data[0], data[1], double.Parse(data[2]));
                }
                else
                {
                    MessageBox.Show("\tWrong File Dumbass!!!\n\nTxt file should be Master Price List\nIn this format A-SKU, B-Description, C-Price.", "Idiot", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    break;
                }
            }
            read.Close();
        }
    }

    protected void Create_Click(Object sender, EventArgs e)
    {
        SaveFileDialog s = new SaveFileDialog();
        s.Filter = "Text File|*.txt";
        if (s.ShowDialog() == DialogResult.OK)
        {
            StreamWriter write = new StreamWriter(s.OpenFile());
            write.WriteLine("SKU\tDescription\tPrice\tTotal Bought\tLeft to Sell\tTotal Invested\tTotal At Amazon\tAmazon Invested\tIn Garage\tInvested Garage\tTotal Sold");
            inventory.InOrderPrint1(write);
            write.Close();
        }

    }

    #endregion

    protected void Update_Click(Object sender, EventArgs e)
    {
        Sku_Label.BackColor = Form.DefaultBackColor;
        change_Label.BackColor = Form.DefaultBackColor;
        SendAmazon.BackColor = Form.DefaultBackColor;
        Total.BackColor = Form.DefaultBackColor;
        View.BackColor = Form.DefaultBackColor;
        if (View.Checked && SKU_Entry.Text != "")
        {
            TreeNode data = inventory.search(SKU_Entry.Text);
            output.Text = "SKU: " + data.SKU + "\nDescription: " + data.Description + "\nPrice: " + string.Format("{0:C}", data.Price) + "\nTotal Quantity: " + data.Total + "\nTotal at Amazon: " + data.Amazon.ToString();
            double totalprice = data.Total * data.Price;
            output.Text += "\nIn Garage: " + data.garage.ToString() + "\nTotal Sold: " + data.sold + "\nTotal Invested: " + string.Format("{0:C}", totalprice);
        }
        else
        {
            int result = 0;
            bool checker = int.TryParse(Change_Total.Text, out result);
            if (SKU_Entry.Text != "" && Change_Total.Text != "" && (SendAmazon.Checked || Total.Checked || View.Checked || total_Sold.Checked) && checker)
            {
                if (Total.Checked)
                {
                    if (remove.Checked)
                    {
                        inventory.AddTotal(SKU_Entry.Text, int.Parse(Change_Total.Text) * -1);
                        inventory.AddGarage(SKU_Entry.Text, int.Parse(Change_Total.Text) * -1);
                    }
                    else
                    {
                        inventory.AddTotal(SKU_Entry.Text, int.Parse(Change_Total.Text));
                        inventory.AddGarage(SKU_Entry.Text, int.Parse(Change_Total.Text));
                    }
                    TreeNode data = inventory.search(SKU_Entry.Text);
                    output.Text = "SKU: " + data.SKU + "\nDescription: " + data.Description + "\nPrice: " + string.Format("{0:C}", data.Price) + "\nTotal Quantity: " + data.Total.ToString() + "\nTotal at Amazon: " + data.Amazon.ToString();
                    double totalprice = data.Total * data.Price;
                    output.Text += "\nIn Garage: " + data.garage + "\nTotal Sold: " + data.sold + "\nTotal Invested: " + string.Format("{0:C}", totalprice);
                }
                else if (SendAmazon.Checked)
                {
                    if (remove.Checked)
                    {
                        inventory.AddAmazon(SKU_Entry.Text, int.Parse(Change_Total.Text) * -1);
                        inventory.AddGarage(SKU_Entry.Text, int.Parse(Change_Total.Text));
                    }
                    else
                    {
                        inventory.AddAmazon(SKU_Entry.Text, int.Parse(Change_Total.Text));
                        inventory.AddGarage(SKU_Entry.Text, int.Parse(Change_Total.Text) * -1);
                    }
                    TreeNode data = inventory.search(SKU_Entry.Text);
                    output.Text = "SKU: " + data.SKU + "\nDescription: " + data.Description + "\nPrice: " + string.Format("{0:C}", data.Price) + "\nTotal Quantity: " + data.Total.ToString() + "\nTotal at Amazon: " + data.Amazon.ToString();
                    double totalprice = data.Total * data.Price;
                    output.Text += "\nIn Garage: " + data.garage.ToString() + "\nTotal Sold: " + data.sold.ToString() + "\nTotal Invested: " + string.Format("{0:C}", totalprice);
                }
                else if (total_Sold.Checked)
                {
                    if (remove.Checked)
                    {
                        inventory.AddSold(SKU_Entry.Text, int.Parse(Change_Total.Text) * -1, true);
                        inventory.AddGarage(SKU_Entry.Text, int.Parse(Change_Total.Text));
                    }
                    else
                    {
                        inventory.AddSold(SKU_Entry.Text, int.Parse(Change_Total.Text), false);
                        inventory.AddAmazon(SKU_Entry.Text, int.Parse(Change_Total.Text) * -1);
                    }
                    TreeNode data = inventory.search(SKU_Entry.Text);
                    output.Text = "SKU: " + data.SKU + "\nDescription: " + data.Description + "\nPrice: " + string.Format("{0:C}", data.Price) + "\nTotal Quantity: " + data.Total.ToString() + "\nTotal at Amazon: " + data.Amazon.ToString();
                    double totalprice = data.Total * data.Price;
                    output.Text += "\nIn Garage: " + data.garage.ToString() + "\nTotal Sold: " + data.sold.ToString() + "\nTotal Invested: " + string.Format("{0:C}", totalprice);
                }
            }
            else
            {
                if (SKU_Entry.Text == "" && View.Checked)
                {
                    Sku_Label.BackColor = Color.Red;
                }
                if (Change_Total.Text == "" || checker)
                {
                    change_Label.BackColor = Color.Red;
                }
            }
        }
        Change_Total.Text = "";
        remove.Checked = false;
        View.Checked = true;
        SKU_Entry.Focus();
    }

    protected void Exit_Click(Object sender, EventArgs e)
    {
        Close();
    }

    protected void Clear_Click(Object sender, EventArgs e)
    {
        Sku_Label.BackColor = Form.DefaultBackColor;
        change_Label.BackColor = Form.DefaultBackColor;
        SendAmazon.BackColor = Form.DefaultBackColor;
        Total.BackColor = Form.DefaultBackColor;
        View.BackColor = Form.DefaultBackColor;
        SendAmazon.Checked = false;
        View.Checked = true;
        Total.Checked = false;
        remove.Checked = false;
        SKU_Entry.Focus();
        SKU_Entry.Text = "";
        Change_Total.Text = "";
        output.Text = "";
    }

    private void LoadData()
    {
        string path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\DataJoe";
        StreamReader read;
        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
            File.Create(path + "\\LoadData.txt").Close();
            File.Create(path + "\\IdStore.txt").Close();
        }
        else
        {
            read = new StreamReader(path + "\\LoadData.txt");
            string line;

            while ((line = read.ReadLine()) != null)
            {
                string[] data = line.Split(new char[] { '\t' }, StringSplitOptions.RemoveEmptyEntries);
                if (data.Length == 11)
                {
                    inventory.insert(data[0], data[1], double.Parse(data[2]), int.Parse(data[3]), int.Parse(data[6]), int.Parse(data[10]), int.Parse(data[8]));
                }
            }
            read.Close();

        }

    }
}