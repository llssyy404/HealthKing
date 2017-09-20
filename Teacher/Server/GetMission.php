<?php
$con = mysqli_connect("localhost","came1230","Healthking1!","came1230");

$SchoolUnique = $_POST["SchoolUnique"];
$Grade = $_POST["Grade"];
$result = mysqli_query($con, "SELECT MissionUnique, MissionDesc 
FROM MISSION 
WHERE SchoolUnique = $SchoolUnique 
AND Grade = $Grade");

$response = array();

while ($row = mysqli_fetch_array($result))
{
	array_push($response, array("MissionUnique" =>$row[0], "MissionDesc" =>$row[1]));
}

echo json_encode(array("response" => $response), JSON_UNESCAPED_UNICODE);
mysqli_close($con);

?>