<?php
$con = mysqli_connect("localhost","came1230","Healthking1!","came1230");

$userID = $_POST["userID"];
$recordDate = $_POST["recordDate"];
$recordMeter = $_POST["recordMeter"];
$trackCount = $_POST["trackCount"];
$trackTimeDate = $_POST["trackTimeDate"];
$allTrackTimeDate = $_POST["allTrackTimeDate"];


$statement = mysqli_prepare($con, "INSERT INTO RECORDDATA VALUES (?, ?, ?, ?, ?, ?)");
mysqli_stmt_bind_param($statement, "ssssss", $userID, $recordDate, $recordMeter, $trackCount, $trackTimeDate, $allTrackTimeDate);
mysqli_stmt_execute($statement);

$response = array();
$response["success"] = true;


echo json_encode($response);

?>