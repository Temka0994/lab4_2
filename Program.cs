using System;

class TSMatrix
{
    protected int[,] matrix;
    protected int rows;
    protected int cols;

    public TSMatrix()
    {
        rows = 0;
        cols = 0;
        matrix = new int[0,0];
    }

    public TSMatrix(int rows, int cols)
    {
        this.rows = rows;
        this.cols = cols;
        matrix = new int[rows, cols];
    }

    public TSMatrix(TSMatrix other)
    {
        rows = other.rows;
        cols = other.cols;
        matrix = new int[rows, cols];
        Array.Copy(other.matrix, matrix, other.matrix.Length);
    }

    public void InputData()
    {
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                Console.Write($"Введіть рядок - {i}, колонку - {j}: ");
                matrix[i, j] = int.Parse(Console.ReadLine());
            }
        }і
    }

    public void OutputData()
    {
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                Console.Write(matrix[i, j] + "\t");
            }
            Console.WriteLine();
        }
    }

    public int FindMax()
    {
        int max = matrix[0, 0];
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                if (matrix[i, j] > max)
                {
                    max = matrix[i, j];
                }
            }
        }
        return max;
    }

    public int FindMin()
    {
        int min = matrix[0, 0];
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                if (matrix[i, j] < min)
                {
                    min = matrix[i, j];
                }
            }
        }
        return min;
    }

    public int FindSum()
    {
        int sum = 0;
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                sum += matrix[i, j];
            }
        }
        return sum;
    }

    public static TSMatrix operator +(TSMatrix a, TSMatrix b)
    {
        if (a.rows != b.rows || a.cols != b.cols)
        {
            throw new InvalidOperationException("Недопустимi розмiри матриць для додавання.");
        }

        TSMatrix result = new TSMatrix(a.rows, a.cols);
        for (int i = 0; i < a.rows; i++)
        {
            for (int j = 0; j < a.cols; j++)
            {
                result.matrix[i, j] = a.matrix[i, j] + b.matrix[i, j];
            }
        }
        return result;
    }

    public static TSMatrix operator -(TSMatrix a, TSMatrix b)
    {
        if (a.rows != b.rows || a.cols != b.cols)
        {
            throw new InvalidOperationException("Недопустимi розмiри матриць для вiднiмання.");
        }

        TSMatrix result = new TSMatrix(a.rows, a.cols);
        for (int i = 0; i < a.rows; i++)
        {
            for (int j = 0; j < a.cols; j++)
            {
                result.matrix[i, j] = a.matrix[i, j] - b.matrix[i, j];
            }
        }
        return result;
    }
}

class TMSMatrix : TSMatrix
{
    public TMSMatrix(int rows, int cols) : base(rows, cols)
    {
    }

    public TMSMatrix(TSMatrix other) : base(other)
    {
    }

    public TMSMatrix Transpose()
    {
        TMSMatrix result = new TMSMatrix(cols, rows);
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                result.matrix[j, i] = matrix[i, j];
            }
        }
        return result;
    }

    public static TMSMatrix operator *(TMSMatrix a, TMSMatrix b)
    {
        if (a.cols != b.rows)
        {
            throw new InvalidOperationException("Недопустимi розмiри матриць для множення");
        }

        TMSMatrix result = new TMSMatrix(a.rows, b.cols);
        for (int i = 0; i < a.rows; i++)
        {
            for (int j = 0; j < b.cols; j++)
            {
                for (int k = 0; k < a.cols; k++)
                {
                    result.matrix[i, j] += a.matrix[i, k] * b.matrix[k, j];
                }
            }
        }
        return result;
    }

    public static TMSMatrix operator *(TMSMatrix a, int scalar)
    {
        TMSMatrix result = new TMSMatrix(a.rows, a.cols);
        for (int i = 0; i < a.rows; i++)
        {
            for (int j = 0; j < a.cols; j++)
            {
                result.matrix[i, j] = a.matrix[i, j] * scalar;
            }
        }
        return result;
    }

    public static TMSMatrix operator +(TMSMatrix a, TMSMatrix b)
    {
        if (a.rows != b.rows || a.cols != b.cols)
        {
            throw new InvalidOperationException("Недопустимi розмiри матриць для додавання.");
        }

        TMSMatrix result = new TMSMatrix(a.rows, a.cols);
        for (int i = 0; i < a.rows; i++)
        {
            for (int j = 0; j < a.cols; j++)
            {
                result.matrix[i, j] = a.matrix[i, j] + b.matrix[i, j];
            }
        }
        return result;
    }

    public static TMSMatrix operator -(TMSMatrix a, TMSMatrix b)
    {
        if (a.rows != b.rows || a.cols != b.cols)
        {
            throw new InvalidOperationException("Розмiри матриць не вiдповiдають один одному для вiднiмання.");
        }

        TMSMatrix result = new TMSMatrix(a.rows, a.cols);
        for (int i = 0; i < a.rows; i++)
        {
            for (int j = 0; j < a.cols; j++)
            {
                result.matrix[i, j] = a.matrix[i, j] - b.matrix[i, j];
            }
        }
        return result;
    }

    public int FindDeterminant()
    {
        if (rows != cols)
        {
            throw new InvalidOperationException("Матриця повинна бути квадратною для знаходження визначника.");
        }

        if (rows == 1)
        {
            return matrix[0, 0];
        }
        else if (rows == 2)
        {
            return matrix[0, 0] * matrix[1, 1] - matrix[0, 1] * matrix[1, 0];
        }
        else
        {
            int determinant = 0;
            for (int i = 0; i < cols; i++)
            {
                determinant += matrix[0, i] * Cofactor(0, i).FindDeterminant() * (i % 2 == 0 ? 1 : -1);
            }
            return determinant;
        }
    }

    private TMSMatrix Cofactor(int row, int col)
    {
        TMSMatrix result = new TMSMatrix(rows - 1, cols - 1);
        int r = 0;
        int c = 0;
        for (int i = 0; i < rows; i++)
        {
            if (i == row)
            {
                continue;
            }
            for (int j = 0; j < cols; j++)
            {
                if (j == col)
                {
                    continue;
                }
                result.matrix[r, c] = matrix[i, j];
                c++;
            }
            r++;
            c = 0;
        }
        return result;
    }
}

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Введiть розмiри першої матрицi:");
        Console.Write("Рядки: ");
        int rows1 = int.Parse(Console.ReadLine());
        Console.Write("Колонки: ");
        int cols1 = int.Parse(Console.ReadLine());

        TMSMatrix matrix1 = new TMSMatrix(rows1, cols1);
        Console.WriteLine("Введiть данi для першої матрицi:");
        matrix1.InputData();

        Console.WriteLine("Введiть розмiри другої матрицi:");
        Console.Write("Рядки: ");
        int rows2 = int.Parse(Console.ReadLine());
        Console.Write("Колонки: ");
        int cols2 = int.Parse(Console.ReadLine());

        TMSMatrix matrix2 = new TMSMatrix(rows2, cols2);
        Console.WriteLine("Enter data for the second matrix:");
        matrix2.InputData();

        Console.WriteLine("Mатриця 1:");
        matrix1.OutputData();
        Console.WriteLine("Матриця 2:");
        matrix2.OutputData();

        TMSMatrix sum = matrix1 + matrix2;
        TMSMatrix diff = matrix1 - matrix2;
        Console.WriteLine("Сума матриць:");
        sum.OutputData();
        Console.WriteLine("Рiзниця матриць:");
        diff.OutputData();

        TMSMatrix transposed = matrix1.Transpose();
        Console.WriteLine("T першої матрицi 1:");
        transposed.OutputData();

        TMSMatrix product = matrix1 * matrix2;
        Console.WriteLine("Добуток матриць:");
        product.OutputData();

        int scalar = 2;
        TMSMatrix scaled = matrix1 * scalar;
        Console.WriteLine($"Помножена перша матриця на {scalar}:");
        scaled.OutputData();

        int determinant = matrix1.FindDeterminant();
        Console.WriteLine($"Детермiнант першої матриці: {determinant}");
    }
}
