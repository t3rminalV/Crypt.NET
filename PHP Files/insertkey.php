<?php

	$DB_NAME = '';
	$DB_HOST = '';
	$DB_USER = '';
	$DB_PASS = '';
	
	$mysqli = new mysqli($DB_HOST, $DB_USER, $DB_PASS, $DB_NAME);
	
	if (mysqli_connect_errno()) {
		printf("Connect failed: %s\n", mysqli_connect_error());
		exit();
	}

	$key = $_GET['key'];
	$user = $_GET['user'];
	
	$query = "SELECT * FROM `keys` WHERE username='$user'";
	$result = $mysqli->query($query) or die($mysqli->error.__LINE__);
		
	if($result->num_rows > 0) 
	{
		$query2 = "UPDATE `keys` SET keycontent='$key' WHERE username='$user'";
		$result2 = $mysqli->query($query2) or die($mysqli->error.__LINE__);
		$return = "updated";
	}
	else 
	{
		$query2 = "INSERT INTO `keys` (username, keycontent) VALUES ('$user', '$key')";
		$result2 = $mysqli->query($query2) or die($mysqli->error.__LINE__);
		$return = "inserted";
	}

	echo $return;

	mysqli_close($mysqli);

?>