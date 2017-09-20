<?php
$con = mysqli_connect("localhost","came1230","Healthking1!","came1230");

$MissionUnique = $_POST["MissionUnique"];
$StudentID = $_POST["ID"];
$result = mysqli_query($con, "INSERT INTO STUDENT_FIN_MISSION VALUES
((SELECT IFNULL(MAX(StudentFinMissionUnique), 0) 
FROM STUDENT_FIN_MISSION sfm)+1,
$MissionUnique, '$StudentID')");

$response = array();
$response["success"] = true;

echo json_encode(array("response" => $response), JSON_UNESCAPED_UNICODE);
mysqli_close($con);

?>