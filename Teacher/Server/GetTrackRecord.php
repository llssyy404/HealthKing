<?php
$con = mysqli_connect("localhost","came1230","Healthking1!","came1230");

$CardiRecordUnique = $_POST["CardiRecordUnique"];
$result = mysqli_query($con, "SELECT * FROM TRACKRECORD WHERE CardiRecordUnique = $CardiRecordUnique");

$response = array();

while ($row = mysqli_fetch_array($result))
{
	array_push($response, array("TrackRecordUnique" =>$row[0], "CardiRecordUnique" =>$row[1], "TrackIndex" =>$row[2], "ElapsedTime"=>$row[3]));
}

echo json_encode(array("response" => $response), JSON_UNESCAPED_UNICODE);
mysqli_close($con);

?>