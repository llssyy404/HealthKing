<?php
$con = mysqli_connect("localhost","came1230","Healthking1!","came1230");

$MissionUnique = $_POST["MissionUnique"];
$StudentID = $_POST["ID"];
$result = mysqli_query($con, "SELECT * 
FROM STUDENT_FIN_MISSION 
WHERE MissionUnique = $MissionUnique
AND StudentID =  '$StudentID'");

$response = array();
$response["Exist"] = false;

while ($row = mysqli_fetch_array($result))
{
    $response["Exist"] = true;
}

echo json_encode(array("response" => $response), JSON_UNESCAPED_UNICODE);
mysqli_close($con);

?>