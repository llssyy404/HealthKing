<?php
$con = mysqli_connect("localhost","came1230","Healthking1!","came1230");

$StudentID = $_POST["ID"];
$result = mysqli_query($con, "SELECT AVG(MUSCRECORD.Count) AS AvgCount
FROM MUSCRECORD, STUDENT 
WHERE MUSCRECORD.StudentID = STUDENT.ID 
AND STUDENT.SchoolUnique = (SELECT SchoolUnique FROM STUDENT WHERE ID = '$StudentID')
AND STUDENT.Grade = (SELECT Grade FROM STUDENT WHERE ID = '$StudentID')
AND STUDENT.Gender = (SELECT Gender FROM STUDENT WHERE ID = '$StudentID')");

$response = array();

while ($row = mysqli_fetch_array($result))
{
	array_push($response, array("AvgCount" =>$row[0]));
}

echo json_encode(array("response" => $response), JSON_UNESCAPED_UNICODE);
mysqli_close($con);

?>