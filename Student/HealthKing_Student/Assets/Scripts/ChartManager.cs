using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using CP.ProChart;

public class ChartManager : MonoBehaviour {

    //bar chart datas
    public BarChart barChart;

    //line chart datas
    public LineChart lineChart;

    //normalDistribution chart datas
    public LineChart normalDistChart;

    //labels
    public GameObject labelBar;
    public GameObject labelLine;
    public GameObject axisXLabel;
    public GameObject axisYLabel;

    //2D Data set
    private ChartData2D dataSet;

    //2D Data set
    private ChartData2D nomalDataSet;

    private List<Text> barLabels = new List<Text>();
    private List<Text> barXLabels = new List<Text>();
    private List<Text> barYLabels = new List<Text>();
    private List<Text> lineLabels = new List<Text>();
    private List<Text> lineXLabels = new List<Text>();
    private List<Text> lineYLabels = new List<Text>();

    ///<summary>
    /// Initialize data set and charts
    ///</summary>
    void OnEnable()
    {
        lineChart.Thickness = 0.01f;
        lineChart.PointSize = 0.02f;
        normalDistChart.Thickness = 0.01f;
        normalDistChart.PointSize = 0.02f;
        normalDistChart.Chart = LineChart.ChartType.CURVE;
        normalDistChart.Point = LineChart.PointType.NONE;

        dataSet = new ChartData2D();
        dataSet[0, 0] = 10;
        dataSet[0, 1] = 20;
        dataSet[0, 2] = 30;
        dataSet[0, 3] = 40;
        dataSet[0, 4] = 50;
        dataSet[0, 5] = 60;
        dataSet[0, 6] = 50;
        dataSet[0, 7] = 40;
        dataSet[1, 0] = 40;
        dataSet[1, 1] = 25;
        dataSet[1, 2] = 53;
        dataSet[1, 3] = 12;
        dataSet[1, 4] = 37;
        dataSet[1, 5] = 58;
        dataSet[1, 6] = 50;
        dataSet[1, 7] = 42;

        nomalDataSet = new ChartData2D();
        nomalDataSet[0, 0] = 5;
        nomalDataSet[0, 1] = 10;
        nomalDataSet[0, 2] = 35;
        nomalDataSet[0, 3] = 75;
        nomalDataSet[0, 4] = 35;
        nomalDataSet[0, 5] = 10;
        nomalDataSet[0, 6] = 5;

        barChart.SetValues(ref dataSet);
        lineChart.SetValues(ref dataSet);
        normalDistChart.SetValues(ref nomalDataSet);

        labelBar.SetActive(false);
        labelLine.SetActive(false);
        axisXLabel.SetActive(false);
        axisYLabel.SetActive(false);

        barLabels.Clear();
        lineLabels.Clear();

        for (int i = 0; i < dataSet.Rows; i++)
        {
            for (int j = 0; j < dataSet.Columns; j++)
            {
                GameObject obj = (GameObject)Instantiate(labelBar);
                obj.name = "Label" + i + "_" + j;
                obj.transform.SetParent(barChart.transform, false);
                Text t = obj.GetComponentInChildren<Text>();
                barLabels.Add(t);

                obj = (GameObject)Instantiate(labelLine);
                obj.name = "Label" + i + "_" + j;
                obj.transform.SetParent(lineChart.transform, false);
                t = obj.GetComponent<Text>();
                lineLabels.Add(t);
            }
        }

        barXLabels.Clear();
        lineXLabels.Clear();

        for (int i = 0; i < dataSet.Columns; i++)
        {
            GameObject obj = (GameObject)Instantiate(axisXLabel);
            obj.name = "Label" + i;
            obj.transform.SetParent(barChart.transform, false);
            Text t = obj.GetComponent<Text>();
            t.text = (i+1) + "바퀴";
            barXLabels.Add(t);

            obj = (GameObject)Instantiate(axisXLabel);
            obj.name = "Label" + i;
            obj.transform.SetParent(lineChart.transform, false);
            t = obj.GetComponent<Text>();
            t.text = (i + 1) + "바퀴";
            lineXLabels.Add(t);
        }

        barYLabels.Clear();
        lineYLabels.Clear();

        for (int i = 0; i < dataSet.Columns; i++)
        {
            GameObject obj = (GameObject)Instantiate(axisYLabel);
            obj.name = "Label" + i;
            obj.transform.SetParent(barChart.transform, false);
            Text t = obj.GetComponent<Text>();
            t.text = t.gameObject.name;
            barYLabels.Add(t);

            obj = (GameObject)Instantiate(axisYLabel);
            obj.name = "Label" + i;
            obj.transform.SetParent(lineChart.transform, false);
            t = obj.GetComponent<Text>();
            t.text = t.gameObject.name;
            lineYLabels.Add(t);
        }
    }

    ///<summary>
    /// Remove hanlders when object is disabled
    ///</summary>
    void OnDisable()
    {

    }

    void Update()
    {
        UpdateLabels();
    }

    ///<summary>
    /// Update labels
    ///</summary>
    public void UpdateLabels()
    {
        for (int i = 0; i < dataSet.Rows; i++)
        {
            for (int j = 0; j < dataSet.Columns; j++)
            {
                LabelPosition labelPos = barChart.GetLabelPosition(i, j, 1.0f);
                if (labelPos != null)
                {
                    barLabels[i * dataSet.Columns + j].transform.parent.gameObject.SetActive(true);
                    barLabels[i * dataSet.Columns + j].text = labelPos.value.ToString("0.00");
                    barLabels[i * dataSet.Columns + j].transform.parent.gameObject.GetComponent<RectTransform>().anchoredPosition = labelPos.position;
                }

                labelPos = lineChart.GetLabelPosition(i, j);
                if (labelPos != null)
                {
                    lineLabels[i * dataSet.Columns + j].gameObject.SetActive(true);
                    lineLabels[i * dataSet.Columns + j].text = labelPos.value.ToString("0.00");
                    lineLabels[i * dataSet.Columns + j].rectTransform.anchoredPosition = labelPos.position;
                }
            }
        }

        LabelPosition[] positions = barChart.GetAxisXPositions();
        if (positions != null)
        {
            for (int i = 0; i < positions.Length; i++)
            {
                barXLabels[i].gameObject.SetActive(true);
                barXLabels[i].GetComponent<RectTransform>().anchoredPosition = positions[i].position;
            }
        }

        positions = barChart.GetAxisYPositions();
        if (positions != null)
        {
            for (int i = 0; i < 5; i++)
            {
                if (positions.Length - 1 < i)
                {
                    barYLabels[i].gameObject.SetActive(false);
                }
                else
                {
                    barYLabels[i].gameObject.SetActive(true);
                    barYLabels[i].text = positions[i].value.ToString("0.0");
                    barYLabels[i].GetComponent<RectTransform>().anchoredPosition = positions[i].position;
                }
            }
        }

        positions = lineChart.GetAxisXPositions();
        if (positions != null)
        {
            for (int i = 0; i < positions.Length; i++)
            {
                lineXLabels[i].gameObject.SetActive(true);
                lineXLabels[i].GetComponent<RectTransform>().anchoredPosition = positions[i].position;
            }
        }

        positions = lineChart.GetAxisYPositions();
        if (positions != null)
        {
            for (int i = 0; i < 5; i++)
            {
                if (positions.Length - 1 < i)
                {
                    lineYLabels[i].gameObject.SetActive(false);
                }
                else
                {
                    lineYLabels[i].gameObject.SetActive(true);
                    lineYLabels[i].text = positions[i].value.ToString("0.0");
                    lineYLabels[i].GetComponent<RectTransform>().anchoredPosition = positions[i].position;
                }
            }
        }
    }
}
