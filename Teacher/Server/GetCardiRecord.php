<?php
$con = mysqli_connect("localhost","came1230","Healthking1!","came1230");

$StudentID = $_POST["ID"];
$result = mysqli_query($con, "SELECT * FROM CARDIRECORD WHERE StudentID = '$StudentID'");

$response = array();

while ($row = mysqli_fetch_array($result))
{
	array_push($response, array("RecordUnique" =>$row[0], "Date" =>$row[2], "TotalMeter"=>$row[3], "TotalTrackCount"=>$row[4], "TotalElapsedTime"=>$row[5]));
}

echo json_encode(array("response" => $response), JSON_UNESCAPED_UNICODE);
mysqli_close($con);

?>