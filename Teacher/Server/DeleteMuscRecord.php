<?php
$con = mysqli_connect("localhost","came1230","Healthking1!","came1230");

$RecordUnique = $_POST["RecordUnique"];
$result = mysqli_query($con, "DELETE 
FROM MUSCRECORD 
WHERE RecordUnique = $RecordUnique");

$response = array();
$response["Success"] = false;

if($result)
{
    $response["Success"] = true;
}

echo json_encode(array("response" => $response), JSON_UNESCAPED_UNICODE);
mysqli_close($con);

?>