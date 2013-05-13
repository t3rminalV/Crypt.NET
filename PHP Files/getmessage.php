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

	$id = $_GET['id'];

	$query = "SELECT postcontent FROM `posts` WHERE postid=$id";

	$result = $mysqli->query($query) or die($mysqli->error.__LINE__);
		
	if($result->num_rows > 0) 
	{		
		while($row = $result->fetch_assoc()) {
			$return = $row['postcontent'];	
		}
	}
	else 
	{		
		$return = "error";
	}

	echo $return;

	mysqli_close($mysqli);

?>