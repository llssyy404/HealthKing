<?php
$con = mysqli_connect("localhost","came1230","Healthking1!","came1230");

$userID = $_POST["userID"];
$userPassword = $_POST["userPassword"];
$userName = $_POST["userName"];
$userGender = $_POST["userGender"];
$userSchool = $_POST["userSchool"];
$userGrade = $_POST["userGrade"];
$userClassroom = $_POST["userClassroom"];


$statement = mysqli_prepare($con, "INSERT INTO USER VALUES (?, ?, ?, ?, ?, ?, ?)");
mysqli_stmt_bind_param($statement, "sssssss", $userID, $userPassword, $userName, $userGender, $userSchool, $userGrade, $userClassroom );
mysqli_stmt_execute($statement);

$response = array();
$response["success"] = true;


echo json_encode($response);

?>