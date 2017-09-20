<?php
$con = mysqli_connect("localhost","came1230","Healthking1!","came1230");

$SchoolUnique = $_POST["SchoolUnique"];
$Grade = $_POST["Grade"];
$Gender = $_POST["Gender"];
$RecordUnique = $_POST["RecordUnique"];
$Count = $_POST["Count"];
$result = mysqli_query($con, "SELECT 
COUNT(IF(Count < $Count, 1, NULL)) AS LowRecordCount,
COUNT(IF(Count = $Count AND RecordUnique <> $RecordUnique, 1, NULL)) AS SameRecordCount,
COUNT( RecordUnique ) AS TotalRecordCount
FROM MUSCRECORD, STUDENT
WHERE MUSCRECORD.StudentID = STUDENT.ID
AND STUDENT.SchoolUnique = $SchoolUnique
AND STUDENT.Grade = $Grade
AND STUDENT.Gender =  '$Gender'");

$response = array();

while ($row = mysqli_fetch_array($result))
{
	array_push($response, array("Percentile" =>100-($row[0]+$row[1]/2)/$row[2]*100 ));
}

echo json_encode(array("response" => $response), JSON_UNESCAPED_UNICODE);
mysqli_close($con);

?>