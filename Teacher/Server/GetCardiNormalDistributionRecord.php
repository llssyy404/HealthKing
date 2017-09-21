<?php
$con = mysqli_connect("localhost","came1230","Healthking1!","came1230");

$StudentID = $_POST["ID"];
$RecordUnique = $_POST["RecordUnique"];
$TotalMeter = $_POST["TotalMeter"];
$TotalTrackCount = $_POST["TotalTrackCount"];
$TotalElapsedTime = $_POST["TotalElapsedTime"];
$result = mysqli_query($con, "SELECT 
COUNT(IF(TotalElapsedTime > $TotalElapsedTime, 1, NULL)) AS LowRecordCount,
COUNT(IF(TotalElapsedTime = $TotalElapsedTime AND RecordUnique <> $RecordUnique, 1, NULL)) AS SameRecordCount,
COUNT( RecordUnique ) AS TotalRecordCount
FROM CARDIRECORD, STUDENT
WHERE TotalMeter = $TotalMeter
AND TotalTrackCount = $TotalTrackCount
AND CARDIRECORD.StudentID = STUDENT.ID
AND STUDENT.SchoolUnique = (SELECT SchoolUnique FROM STUDENT WHERE ID = '$StudentID')
AND STUDENT.Grade = (SELECT Grade FROM STUDENT WHERE ID = '$StudentID')
AND STUDENT.Gender = (SELECT Gender FROM STUDENT WHERE ID = '$StudentID')");

$response = array();

while ($row = mysqli_fetch_array($result))
{
	array_push($response, array("Percentile" =>100-($row[0]+$row[1]/2)/$row[2]*100 ));
}

echo json_encode(array("response" => $response), JSON_UNESCAPED_UNICODE);
mysqli_close($con);

?>