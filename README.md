<h1>King Price Demo</h1>

<h2>Description</h2>
<p> This demo application serves as a prototype for interview scenarios. 
It is a straightforward web-based platform that enables the creation and management of users and groups, 
showcasing simple administrative functionalities and basic CRUD operations. </p>

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