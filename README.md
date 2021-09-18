# winSHCDecoder

<h3>SHC (SmartHealth Card) Decoder written in C#</h3>
<p>Ported from <a href="https://github.com/obrassard/shc-extractor" target="_new">shc-extractor</a></p>
<h4>.NET Assemblies used</h4>
<ul>
	<li>Newtonsoft.Json.13.0.1</li>
	<li>System.Text.RegularExpressions.4.3.1</li>
        <li>System.IO.Compression</li>
</ul>
<p>This is strictly an educational resource to demonstrate the translation of SHC numeric data (from a QR code) into a JWS (JSON) payload.</p>
<p>It contains no error handling, no validation or QR Imagery functionality, this is only a basic example of code that can be improved upon.</p>
<p>A quick and dirty example usage
	<ol>
		<li>Scan QR code with a mobile phone or QR desktop application to get the SHC:/ numeric data.</li>
		<li>Paste the SHC:/012345456789... numeric data into the txtInput textbox on Form1</li>
		<li>Click the button to run the ParseSHC function</li>
	</ol>
</p>
<p>More information regarding the SHC Specifications can be found <a href="https://spec.smarthealth.cards/" target="_new">HERE.</a></p>
