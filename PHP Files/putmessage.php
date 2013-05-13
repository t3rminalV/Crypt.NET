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

	$post = $_GET['post'];
	$user = $_GET['user'];		
	 
	$query = "INSERT INTO `posts` (username, postcontent) VALUES ('$user', '$post')";
	$result = $mysqli->query($query) or die($mysqli->error.__LINE__);
	
	
	$query2 = "SELECT postid FROM `posts` WHERE postcontent='$post'";
	$result2 = $mysqli->query($query2) or die($mysqli->error.__LINE__);
	if($result2->num_rows > 0) 
	{		
		while($row = $result2->fetch_assoc()) {
			$return = $row['postid'];	
		}
	}
	else 
	{		
		$return = "error";
	}	

	echo $return;
	
	mysqli_close($mysqli);

?>