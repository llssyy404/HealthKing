<?php
$con = mysqli_connect("localhost","came1230","Healthking1!","came1230");

$SchoolUnique = $_POST["SchoolUnique"];
$Grade = $_POST["Grade"];
$Gender = $_POST["Gender"];
$TotalMeter = $_POST["TotalMeter"];
$TotalTrackCount = $_POST["TotalTrackCount"];
$result = mysqli_query($con, "SELECT TRACKRECORD.TrackIndex, AVG(TRACKRECORD.ElapsedTime) AS PerTrackElapsedTime
FROM TRACKRECORD, CARDIRECORD, STUDENT 
WHERE TRACKRECORD.CardiRecordUnique = CARDIRECORD.RecordUnique 
AND CARDIRECORD.StudentID = STUDENT.ID
AND STUDENT.SchoolUnique = $SchoolUnique
AND STUDENT.Grade = $Grade
AND STUDENT.Gender = '$Gender'
AND CARDIRECORD.TotalMeter = $TotalMeter 
AND CARDIRECORD.TotalTrackCount = $TotalTrackCount 
GROUP BY TRACKRECORD.TrackIndex 
ORDER BY TRACKRECORD.TrackIndex ASC");

$response = array();

while ($row = mysqli_fetch_array($result))
{
	array_push($response, array("TrackIndex" =>$row[0], "PerTrackElapsedTime" =>$row[1]));
}

echo json_encode(array("response" => $response), JSON_UNESCAPED_UNICODE);
mysqli_close($con);

?>