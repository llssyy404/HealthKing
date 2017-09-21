<?php
$con = mysqli_connect("localhost","came1230","Healthking1!","came1230");

$StudentID = $_POST["ID"];
$result = mysqli_query($con, "SELECT * FROM MUSCRECORD WHERE StudentID = '$StudentID'");

$response = array();

while ($row = mysqli_fetch_array($result))
{
	array_push($response, array("RecordUnique" =>$row[0], "Date" =>$row[2], "Count"=>$row[3]));
}

echo json_encode(array("response" => $response), JSON_UNESCAPED_UNICODE);
mysqli_close($con);

?>