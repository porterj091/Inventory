using System;
using System.IO;


public class IDNode
{
    public string ID;
    public IDNode right;
    public IDNode left;
    public IDNode()
    {
        this.ID = "";
        this.left = null;
        this.right = null;
    }

    public IDNode(string _id)
    {
        this.ID = _id;
        this.right = null;
        this.left = null;
    }

}

public class ID_Tree
{
    public IDNode root;

    public ID_Tree()
    {
        this.root = null;
    }

    #region Public User Functions
    public void insert(string _id)
    {
        insert(ref root, _id);
    }

    public bool search(string sku)
    {
        IDNode tree = root;

        while (tree != null)
        {
            if (tree.ID == sku)
                return true;
            else if (sku.CompareTo(tree.ID) < 0)
                tree = tree.left;
            else
                tree = tree.right;
        }

        return false;
    }

    public void PreOrderPrint()
    {
        string path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\DataJoe";
        StreamWriter write = new StreamWriter(path + "\\IdStore.txt");
        PreOrderPrint(ref root, write);
        write.Close();
    }
    #endregion


    #region Utility Functions
    private void insert(ref IDNode tree, string _id)
    {
        if (tree == null)
        {
            tree = new IDNode(_id);
        }

        if (tree.ID == _id)
            return;

        if (_id.CompareTo(tree.ID) < 0)
        {
            insert(ref tree.left, _id);
            balance_tree(ref tree);
        }
        else
        {
            insert(ref tree.right, _id);
            balance_tree(ref tree);
        }
    }

    private void remove(ref IDNode tree, string sku)
    {
        if (tree == null)
            return;
        if (sku.CompareTo(tree.ID) < 0)
        {
            remove(ref tree.left, sku);
        }
        else if (sku.CompareTo(tree.ID) > 0)
        {
            remove(ref tree.right, sku);
        }
        else
            makeDelete(ref tree);
    }

    private void makeDelete(ref IDNode tree)
    {
        IDNode nodetoDelete = tree;
        IDNode attachPoint;

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

    private void balance_tree(ref IDNode current)
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

    private int getHeight(ref IDNode current)
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

    private int balance_factor(ref IDNode current)
    {
        int l = getHeight(ref current.left);
        int r = getHeight(ref current.right);
        int b_factor = l - r;
        return b_factor;
    }

    private IDNode RotateRR(ref IDNode parent)
    {
        IDNode pivot = parent.right;
        parent.right = pivot.left;
        pivot.left = parent;
        return pivot;
    }

    private IDNode RotateLL(ref IDNode parent)
    {
        IDNode pivot = parent.left;
        parent.left = pivot.right;
        pivot.right = parent;
        return pivot;
    }

    private IDNode RotateLR(ref IDNode parent)
    {
        IDNode pivot = parent.left;
        parent.left = RotateRR(ref pivot);
        return RotateLL(ref parent);
    }

    private IDNode RotateRL(ref IDNode parent)
    {
        IDNode pivot = parent.right;
        parent.right = RotateLL(ref pivot);
        return RotateRR(ref parent);
    }

    private void PreOrderPrint(ref IDNode tree, StreamWriter write)
    {
        if (tree != null)
        {
            write.WriteLine(tree.ID.ToString());
            PreOrderPrint(ref tree.left, write);
            PreOrderPrint(ref tree.right, write);
        }
    }

    #endregion




}