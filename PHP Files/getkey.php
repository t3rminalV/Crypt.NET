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

	$user = $_GET['user'];

	$query = "SELECT keycontent FROM `keys` WHERE username='$user'";

	$result = $mysqli->query($query) or die($mysqli->error.__LINE__);
		
	if($result->num_rows > 0) 
	{		
		while($row = $result->fetch_assoc()) {
			$return = $row['keycontent'];	
		}
	}
	else 
	{		
		$return = "error";
	}

	echo $return;

	mysqli_close($mysqli);

?>