using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Laba_12
{
    public partial class Form1 : Form
    {
        String[] arraySortNames = new String[] { "Обмен", "Выбор", "Включение", "Шелла", "Быстрая", "Линейная", "Встроенная" };
        const int ROWS_COUNT = 7;
        int arraySize;
        int[] array;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            dataGridView1.RowCount = ROWS_COUNT;
            for (int i = 0; i < ROWS_COUNT; i++)
            {
                dataGridView1.Rows[i].Cells[1].Value = arraySortNames[i];
                dataGridView1.Rows[i].Cells[0].Value = false;
            }
        }

        private void buttonSort_Click(object sender, EventArgs e)
        {
            Random rnd = new Random();
            arraySize = (int)numericUpDown_arraySize.Value;
            array = new int[arraySize];
            for (int i = 0; i < array.Length; i++) array[i] = rnd.Next(1, 10);

            for (int i = 0; i < ROWS_COUNT; i++)
            {
                if ((bool)dataGridView1.Rows[i].Cells[0].Value)
                {
                    switch (i)
                    {
                        case 0:
                            {
                                addValuesTable(BubbleSort(array), i);
                                break;
                            }
                        case 1:
                            {
                                addValuesTable(SortDirectSelection(array), i);
                                break;
                            }
                        default: break;
                    }
                }
            }
        }
        public InfoSort BubbleSort(int[] array)
        {
            int comparisons = 0;
            int permutations = 0;

            // Создаётся копия массива, дабы не сортировать исходный.
            int[] arrayCopy = new int[array.Length];
            Array.Copy(array, arrayCopy, array.Length);
            int StartTime = Environment.TickCount;
            {
                bool swapped;
                for (int i = 0; i < arrayCopy.Length - 1; i++)
                {
                    swapped = false;
                    for (int j = 0; j < arrayCopy.Length - i - 1; j++)
                    {
                        if (arrayCopy[j] > arrayCopy[j + 1])
                        {
                            int temp = arrayCopy[j];
                            arrayCopy[j] = arrayCopy[j + 1];
                            arrayCopy[j + 1] = temp;
                            swapped = true;
                            permutations++;
                        }
                        comparisons++;
                    }
                    // Если на каком-то проходе не было обменов, массив отсортирован
                    if (!swapped)
                        break;
                }
                int ResultTime = Environment.TickCount - StartTime;
                return new InfoSort { Time = ResultTime, Comparisons = comparisons, Permutations = permutations };
            }
        }

        public InfoSort SortDirectSelection(int[] _arr)
        {
            int comparisons = 0; // Кол-во сравнений.
            int permutations = 0; // Кол-во перестановок.

            int[] arr = new int[_arr.Length]; // Создаётся копия массива, дабы не сортировать исходный.
            for (int i = 0; i < arr.Length; i++)
            {
                arr[i] = _arr[i];
            }

            int StartTime = Environment.TickCount;
            {
                for (int i = 0; i < arr.Length - 1; i++)
                {
                    int minIndex = i; // Индекс минимального элемента в неотсортированной части массива.

                    for (int j = i + 1; j < arr.Length; j++)  // Поиск минимального элемента в неотсортированной части массива
                    {
                        if (arr[j] < arr[minIndex])
                        {
                            minIndex = j;
                        }
                        comparisons++;
                    }

                    // Swap.
                    int temp = arr[i];
                    arr[i] = arr[minIndex];
                    arr[minIndex] = temp;
                    permutations++;
                }
            }
            int ResultTime = Environment.TickCount - StartTime;
            return new InfoSort { Time = ResultTime, Comparisons = comparisons, Permutations = permutations };
        }

        public void addValuesTable(InfoSort sort, int index)
        {
            dataGridView1.Rows[index].Cells[4].Value = sort.Time;
            dataGridView1.Rows[index].Cells[3].Value = sort.Permutations;
            dataGridView1.Rows[index].Cells[2].Value = sort.Comparisons;
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            Close();
        }
    }
}