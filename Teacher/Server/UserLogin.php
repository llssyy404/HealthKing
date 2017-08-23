<?php
$con = mysqli_connect("localhost","came1230","Healthking1!","came1230");

$userID = $_POST["userID"];
$userPassword = $_POST["userPassword"];

$result = mysqli_query($con, "SELECT * FROM USER WHERE userID = '$userID' AND userPassword = '$userPassword'");

$response = array();

while ($row = mysqli_fetch_array($result))
{
	array_push($response, array( "userID" =>$row[0], "userPassword" =>$row[1], "userName"=>$row[2], "userGender"=>$row[3], "userSchoolName"=>$row[4], "userSchool"=>$row[5], "userGrade"=>$row[6], "userClassroom"=>$row[7], "userNumber"=>$row[8] ));
}

echo json_encode(array("response" => $response), JSON_UNESCAPED_UNICODE);
mysqli_close($con);

?>