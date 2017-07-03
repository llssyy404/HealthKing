<?php
$con = mysqli_connect("localhost","came1230","Healthking1!","came1230");
$result = mysqli_query($con, "SELECT * FROM RECORDDATA;");
$response = array();

while ($row = mysqli_fetch_array($result))
{
	array_push($response, array("userID" =>$row[0], "recordDate" =>$row[1], "recordMeter"=>$row[2], "trackCount"=>$row[3], "trackTimeDate"=>$row[4], "allTrackTimeDate"=>$row[5] ));
}

echo json_encode(array("response" => $response), JSON_UNESCAPED_UNICODE);
mysqli_close($con);

?>