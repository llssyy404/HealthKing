<?php
$con = mysqli_connect("localhost","came1230","Healthking1!","came1230");

$SchoolUnique = $_POST["SchoolUnique"];
$Grade = $_POST["Grade"];
$Gender = $_POST["Gender"];
$Meter = $_POST["Meter"];
$result = mysqli_query($con, "SELECT AVG(AGILERECORD.ElapsedTime) AS AvgElapsedTime
FROM AGILERECORD, STUDENT 
WHERE AGILERECORD.StudentID = STUDENT.ID 
AND STUDENT.SchoolUnique = $SchoolUnique
AND STUDENT.Grade = $Grade
AND STUDENT.Gender = '$Gender'
AND AGILERECORD.Meter = $Meter");

$response = array();

while ($row = mysqli_fetch_array($result))
{
	array_push($response, array("AvgElapsedTime" =>$row[0]));
}

echo json_encode(array("response" => $response), JSON_UNESCAPED_UNICODE);
mysqli_close($con);

?>