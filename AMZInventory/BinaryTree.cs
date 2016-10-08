using System;
using System.IO;

public class TreeNode
{
    public string SKU;
    public string Description;
    public int Total;
    public int Amazon;
    public int sold;
    public int garage;
    public double Price;
    public TreeNode left;
    public TreeNode right;
    public TreeNode(string _name, string _des, double _price)
    {
        this.SKU = _name;
        this.Price = _price;
        this.Description = _des;
        this.Total = 0;
        this.Amazon = 0;
        left = null;
        right = null;
    }

    public TreeNode(string name, string des, double price, int total, int amazon, int sold, int garage)
    {
        this.SKU = name;
        this.Price = price;
        this.Description = des;
        this.Total = total;
        this.Amazon = amazon;
        this.sold = sold;
        this.garage = garage;
        left = null;
        right = null;
    }
}

public class BSTree
{
    public TreeNode root;

    public BSTree()
    {
        root = null;
    }

    #region Public User Functions
    public void insert(string sku, string des, double price)
    {
        insert(ref root, sku, des, price);
    }
    public void insert(string sku, string des, double price, int total, int amazon, int sold, int garage)
    {
        insert(ref root, sku, des, price, total, amazon, sold, garage);
    }

    public TreeNode search(string sku)
    {
        TreeNode tree = root;

        while (tree != null)
        {
            if (tree.SKU == sku)
                return tree;
            else if (sku.CompareTo(tree.SKU) < 0)
                tree = tree.left;
            else
                tree = tree.right;
        }

        return new TreeNode("Not Found", "Not Found", 0.00);
    }

    public void remove(string sku)
    {
        remove(ref root, sku);
    }

    public void InOrderPrint()
    {
        string path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\DataJoe";
        StreamWriter write = new StreamWriter(path + "\\LoadData.txt");
        InOrderPrint(ref root, write);
        write.Close();
    }

    public void InOrderPrint1(StreamWriter write)
    {
        InOrderPrint1(ref root, write);
    }

    #endregion

    #region Add Functions
    public void AddTotal(string sku, int total)
    {
        TreeNode tree = search(sku);
        tree.Total += total;
        if (tree.Total < 0)
        {
            tree.Total = 0;
        }
    }

    public void AddSold(string sku, int sold, bool remove)
    {
        TreeNode tree = search(sku);
        tree.sold += sold;
        if (tree.sold < 0)
        {
            tree.sold = 0;
        }
        if (tree.Amazon == 0)
        {
            tree.garage -= sold;
        }
        if (sold > tree.Amazon && tree.Amazon != 0)
        {
            tree.garage -= (sold - tree.Amazon);
        }

    }

    public void AddAmazon(string sku, int amazon)
    {
        TreeNode tree = search(sku);
        tree.Amazon += amazon;
        if (tree.Amazon < 0)
        {
            tree.Amazon = 0;
        }
    }

    public void AddGarage(string sku, int garage)
    {
        TreeNode tree = search(sku);
        tree.garage += garage;

        if (tree.garage > tree.Total )
        {
            tree.garage = tree.Total;
        }
    }

    #endregion

    #region Utility Functions
    private void insert(ref TreeNode tree, string _sku, string _des, double price)
    {
        if (tree == null)
        {
            tree = new TreeNode(_sku, _des, price);
        }

        if (tree.SKU == _sku)
        {
            if (tree.Price != price)
                tree.Price = price;
            if (tree.Description != _des)
                tree.Description = _des;
            return;
        }
        if (_sku.CompareTo(tree.SKU) < 0)
        {
            insert(ref tree.left, _sku, _des, price);
            balance_tree(ref tree);
        }
        else
        {
            insert(ref tree.right, _sku, _des, price);
            balance_tree(ref tree);
        }
    }
    private void insert(ref TreeNode tree, string _sku, string _des, double price, int total, int amazon, int sold, int garage)
    {
        if (tree == null)
        {
            tree = new TreeNode(_sku, _des, price, total, amazon, sold, garage);
        }

        if (tree.SKU == _sku)
        {
            if (tree.Price != price)
                tree.Price = price;
            if (tree.Description != _des)
                tree.Description = _des;
            return;
        }
        if (_sku.CompareTo(tree.SKU) < 0)
        {
            insert(ref tree.left, _sku, _des, price, total, amazon, sold, garage);
            balance_tree(ref tree);
        }
        else
        {
            insert(ref tree.right, _sku, _des, price, total, amazon, sold, garage);
            balance_tree(ref tree);
        }
    }

    private void remove(ref TreeNode tree, string sku)
    {
        if (tree == null)
            return;
        if (sku.CompareTo(tree.SKU) < 0)
        {
            remove(ref tree.left, sku);
        }
        else if (sku.CompareTo(tree.SKU) > 0)
        {
            remove(ref tree.right, sku);
        }
        else
            makeDelete(ref tree);
    }

    private void makeDelete(ref TreeNode tree)
    {
        TreeNode nodetoDelete = tree;
        TreeNode attachPoint;

        if (tree.right == null)
        {
            tree = tree.right;
        }
        else if (tree.left == null)
        {
            tree = tree.left;
        }
        else
        {
            attachPoint = tree.right;

            while (attachPoint.left != null)
            {
                attachPoint = attachPoint.left;
            }

            attachPoint.left = tree.left;
            tree = tree.right;
        }
    }

    private void balance_tree(ref TreeNode current)
    {
        int b_factor = balance_factor(ref current);
        if (b_factor > 1)
        {
            if (balance_factor(ref current.left) > 0)
            {
                current = RotateLL(ref current);
            }
            else
            {
                current = RotateLR(ref current);
            }
        }
        else if (b_factor < -1)
        {
            if (balance_factor(ref current.right) > 0)
            {
                current = RotateRL(ref current);
            }
            else
            {
                current = RotateRR(ref current);
            }
        }
    }

    private int getHeight(ref TreeNode current)
    {
        int height = 0;
        if (current != null)
        {
            int l = getHeight(ref current.left);
            int r = getHeight(ref current.right);
            int m = Math.Max(l, r);
            height = m + 1;
        }
        return height;
    }

    private int balance_factor(ref TreeNode current)
    {
        int l = getHeight(ref current.left);
        int r = getHeight(ref current.right);
        int b_factor = l - r;
        return b_factor;
    }

    private TreeNode RotateRR(ref TreeNode parent)
    {
        TreeNode pivot = parent.right;
        parent.right = pivot.left;
        pivot.left = parent;
        return pivot;
    }

    private TreeNode RotateLL(ref TreeNode parent)
    {
        TreeNode pivot = parent.left;
        parent.left = pivot.right;
        pivot.right = parent;
        return pivot;
    }

    private TreeNode RotateLR(ref TreeNode parent)
    {
        TreeNode pivot = parent.left;
        parent.left = RotateRR(ref pivot);
        return RotateLL(ref parent);
    }

    private TreeNode RotateRL(ref TreeNode parent)
    {
        TreeNode pivot = parent.right;
        parent.right = RotateLL(ref pivot);
        return RotateRR(ref parent);
    }

    private void InOrderPrint(ref TreeNode tree, StreamWriter write)
    {
        if (tree != null)
        {
            InOrderPrint(ref tree.left, write);
            write.WriteLine(tree.SKU + "\t" + tree.Description + "\t" + tree.Price + "\t" + tree.Total.ToString() + "\t" + string.Format("{0}", tree.Total - tree.sold) + "\t" + string.Format("{0:C}", tree.Total * tree.Price) + "\t" + tree.Amazon.ToString() + "\t"
                + string.Format("{0:C}", tree.Amazon * tree.Price) + "\t" + tree.garage + "\t" + string.Format("{0:C}", tree.garage * tree.Price)
                + "\t" + tree.sold + "\n");
            InOrderPrint(ref tree.right, write);
        }
    }
    private void InOrderPrint1(ref TreeNode tree, StreamWriter write)
    {
        if (tree != null)
        {
            InOrderPrint1(ref tree.left, write);
            write.WriteLine(tree.SKU + "\t" + tree.Description + "\t" + string.Format("{0:C}", tree.Price) + "\t" + tree.Total.ToString() + "\t" + string.Format("{0}", tree.Total - tree.sold) + "\t" + string.Format("{0:C}", tree.Total * tree.Price) + "\t" + tree.Amazon.ToString() + "\t"
                + string.Format("{0:C}", tree.Amazon * tree.Price) + "\t" + tree.garage + "\t" + string.Format("{0:C}", tree.garage * tree.Price) + "\t" + tree.sold.ToString()
                + "\t" + "\n");
            InOrderPrint1(ref tree.right, write);
        }
    }

    #endregion
}