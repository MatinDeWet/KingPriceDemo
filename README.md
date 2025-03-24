<h1>King Price Demo</h1>

<h2>Description</h2>
<p> This demo application serves as a prototype for interview scenarios. 
It is a straightforward web-based platform that enables the creation and management of users and groups, 
showcasing simple administrative functionalities and basic CRUD operations. </p>

<h3>Running the project</h3>
To run this project you need to set the following startup projects:
<ul>
	<li>docker-compose</li>
	<li>KingPriceDemo.WebClient</li>
</ul>

<h3>Project Endpoints</h3>
<table>
	<tr>
		<th>Web Client</th>
		<td><a href="https://localhost:7001/" target="_blank">https://localhost:7001/</a></td>
	</tr>
	<tr>
		<th>Web API open api spec</th>
		<td><a href="https://localhost:6001/openapi/v1.json" target="_blank">https://localhost:6001/openapi/v1.json</a></td>
	</tr>
	<tr>
		<th>Web API scalar</th>
		<td><a href="https://localhost:6001/scalar/v1" target="_blank">https://localhost:6001/scalar/v1</a></td>
	</tr>
	<tr>
		<th>Sec</th>
		<td><a href="http://localhost:4002/" target="_blank">http://localhost:4002/</a></td>
	</tr>
</table>



<h2>Service Ports</h2>
<table>
	<tr>
		<th>Service</th>
		<th>Local Port</th>
		<th>Container External Port</th>
		<th>Container Internal Port</th>
	</tr>
	<tr>
		<td>SQL Server</td>
		<td>-</td>
		<td>4001</td>
		<td>1433</td>
	</tr>
	<tr>
		<td>Seq</td>
		<td>-</td>
		<td>4002</td>
		<td>80</td>
	</tr>
	<tr>
		<td>King Price Web API</td>
		<td>7001</td>
		<td>
			<table>
				<tr>
					<th>HTTP</th>
					<th>HTTPS</th>
				</tr>
				<tr>
					<td>5001</td>
					<td>6001</td>
				</tr>
			</table>
		</td>
		<td>
			<table>
				<tr>
					<th>HTTP</th>
					<th>HTTPS</th>
				</tr>
				<tr>
					<td>8080</td>
					<td>8081</td>
				</tr>
			</table>
		</td>
	</tr>
	<tr>
		<td>King Price Web Client</td>
		<td>7002</td>
		<td>
			-
		</td>
		<td>
			-
		</td>
	</tr>
</table>