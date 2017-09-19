<?php
$con = mysqli_connect("localhost","came1230","Healthking1!","came1230");

$StudentID = $_POST["ID"];
$StudentPassword = $_POST["Password"];

$result = mysqli_query($con, "SELECT STUDENT.ID, STUDENT.SchoolUnique, SCHOOL.SchoolName, SCHOOL.SchoolGrade, STUDENT.Grade, STUDENT.Class, STUDENT.Number, STUDENT.Gender, STUDENT.Name FROM STUDENT, SCHOOL WHERE ID = '$StudentID' AND Password = '$StudentPassword'");

$response = array();

while ($row = mysqli_fetch_array($result))
{
	array_push($response, array( "ID" =>$row[0], "SchoolUnique" =>$row[1], "SchoolName"=>$row[2], "SchoolGrade"=>$row[3], "Grade"=>$row[4], "Class"=>$row[5], "Number"=>$row[6], "Gender"=>$row[7], "Name"=>$row[8] ));
}

echo json_encode(array("response" => $response), JSON_UNESCAPED_UNICODE);
mysqli_close($con);

?>