<?php
$con = mysqli_connect("localhost","came1230","Healthking1!","came1230");

$SchoolUnique = $_POST["SchoolUnique"];
$Grade = $_POST["Grade"];
$Gender = $_POST["Gender"];
$RecordUnique = $_POST["RecordUnique"];
$TotalMeter = $_POST["TotalMeter"];
$TotalTrackCount = $_POST["TotalTrackCount"];
$TotalElapsedTime = $_POST["TotalElapsedTime"];
$result = mysqli_query($con, "SELECT 
COUNT(IF(TotalElapsedTime < $TotalElapsedTime, 1, NULL)) AS LowRecordCount,
COUNT(IF(TotalElapsedTime = $TotalElapsedTime AND RecordUnique <> $RecordUnique, 1, NULL)) AS SameRecordCount,
COUNT( RecordUnique ) AS TotalRecordCount
FROM CARDIRECORD, STUDENT
WHERE TotalMeter = $TotalMeter
AND TotalTrackCount = $TotalTrackCount
AND CARDIRECORD.StudentID = STUDENT.ID
AND STUDENT.SchoolUnique = $SchoolUnique
AND STUDENT.Grade = $Grade
AND STUDENT.Gender =  '$Gender'");

$response = array();

while ($row = mysqli_fetch_array($result))
{
	array_push($response, array("Percentile" =>($row[0]+$row[1]/2)/$row[2]*100 ));
}

echo json_encode(array("response" => $response), JSON_UNESCAPED_UNICODE);
mysqli_close($con);

?>