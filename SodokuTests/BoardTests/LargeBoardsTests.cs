using Microsoft.VisualStudio.TestTools.UnitTesting;
using Sodoku.IO;
using Sodoku;
using static Sodoku.GlobalConstants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SodokuTests.BoardTests
{
    [TestClass]
    public class LargeBoardsTests
    {
        /// <summary>
        /// 16x16 empty board
        /// </summary>
        [TestMethod]
        public void SixteenOnSixteenEmptyBoardTest()
        {
            // ARRANGE
            string unsolvedBoard = "0000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000";
            string solvedBoard = "123456789:;<=>?@5678=>?@12349:;<9:;<1234=>?@5678=>?@9:;<56781234241389:5>;<6@?=7?;<>@4162=57839:7=@9>;2?83:164<585:637<=?@49;12>31826597;4@=:<>?<74=;?>1398:2@56>@9:4<=2651?378;6?5;:8@37<2>491=4861<35;:7>2?=@9:3=72@89<?65>;41;9>?714:@8=3<562@<25?=6>419;78:3";
            UpdateConstants((int)Math.Sqrt(Math.Sqrt(unsolvedBoard.Length)));
            int[] unsolvedBoardAsArray = InputUtils.InputParser(unsolvedBoard);
            var solver = new SodokuSolver(unsolvedBoardAsArray);

            // ACT
            solver.SolveSodoku();
            string result = solver.ReturnBoardAsString();

            // ASSERT
            Assert.AreEqual(solvedBoard, result);
        }

        /// <summary>
        /// 16x16 easy board
        /// </summary>
        [TestMethod]
        public void SixteenOnSixteenEasyBoardTest()
        {
            // ARRANGE
            string unsolvedBoard = "10023400<06000700080007003009:6;0<00:0010=0;00>0300?200>000900<0=000800:0<201?000;76000@000?005=000:05?0040800;0@0059<00100000800200000=00<580030=00?0300>80@000580010002000=9?000<406@0=00700050300<0006004;00@0700@050>0010020;1?900=002000>000>000;0200=3500<";
            string solvedBoard = "15:2349;<@6>?=78>@8=5?7<43129:6;9<47:@618=?;35>236;?2=8>75:94@<1=4>387;:5<261?@98;76412@9:>?<35=<91:=5?634@8>2;7@?259<>31;7=:68462@>;94=?1<587:37=91?235;>8:@<46583;1:<7264@=9?>?:<4>6@8=9372;152358<>:?6794;1=@:7=<@359>8;1642?;1?968=4@25<7>3:4>6@7;12:?=3589<";
            UpdateConstants((int)Math.Sqrt(Math.Sqrt(unsolvedBoard.Length)));
            int[] unsolvedBoardAsArray = InputUtils.InputParser(unsolvedBoard);
            var solver = new SodokuSolver(unsolvedBoardAsArray);

            // ACT
            solver.SolveSodoku();
            string result = solver.ReturnBoardAsString();

            // ASSERT
            Assert.AreEqual(solvedBoard, result);
        }

        /// <summary>
        /// 16x16 hard board
        /// </summary>
        [TestMethod]
        public void SixteenOnSixteenHardBoard1Test()
        {
            // ARRANGE
            string unsolvedBoard = "0000:=000000000?70050;01:00@90<8900800700004600=60:=080000070002=00030890>?500012;01@:000008007>00001000@0000900000<>?0740000000006@900000>0100002;0600=800<00500070002000000000<0900>?5;4020=0@0020=0@0<0907000>?500400=6000<803<0000002001@:0000=68000?0004020";
            string solvedBoard = ";412:=6@3<8957>?7>?52;41:=6@93<893<8?57>12;46@:=6@:=<893>?57;412=6@:3<897>?52;412;41@:=693<8?57>57>?12;4@:=6893<893<>?57412;=6@::=6@93<857>?12;412;46@:=893<>?57?57>412;6@:=<893<8937>?5;412:=6@412;=6@:<8937>?5>?57;412=6@:3<893<8957>?2;41@:=6@:=6893<?57>412;";
            UpdateConstants((int)Math.Sqrt(Math.Sqrt(unsolvedBoard.Length)));
            int[] unsolvedBoardAsArray = InputUtils.InputParser(unsolvedBoard);
            var solver = new SodokuSolver(unsolvedBoardAsArray);

            // ACT
            solver.SolveSodoku();
            string result = solver.ReturnBoardAsString();

            // ASSERT
            Assert.AreEqual(solvedBoard, result);
        }

        /// <summary>
        /// 16x16 unsolvable board
        /// </summary>
        [TestMethod]
        public void SixteenOnSixteenUnsolvableBoard1Test()
        {
            // ARRANGE
            string unsolvedBoard = ";0?0=>010690000000710000500:?0;4000000<0400070=005<3000800000000500@000:?80>10004<30>?8;00=20000>?8;270060000000000000900000000?0000?00000>0=000?3:0000>0026000000;>61029@0<00000100<0@00:40000800500:0?;>012600800?0;0000090<0@0;07000005<00?8:00003050:4080709";
            UpdateConstants((int)Math.Sqrt(Math.Sqrt(unsolvedBoard.Length)));
            int[] unsolvedBoardAsArray = InputUtils.InputParser(unsolvedBoard);
            var solver = new SodokuSolver(unsolvedBoardAsArray);

            // ACT & ASSERT
            Assert.IsFalse(solver.SolveSodoku());
        }

        /// <summary>
        /// 25x25 empty board
        /// </summary>
        [TestMethod]
        public void TwentyFiveOnTwentyFiveEmptyBoardTest()
        {
            // ARRANGE
            string unsolvedBoard = "0000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000";
            string solvedBoard = "123456789:;<=>?@ABCDEFGHI6789:EFGHI12345;<=>?@ABCD;<=>?12345@ABCDEFGHI6789:@ABCD;<=>?EFGHI6789:12345EFGHI@ABCD6789:12345;<=>?25134:;<=6BE>?7FGC@8IHDA9ADEFGH>517I3689<=;?2:@4BC:@C69?GEI4F=<DH>BA137528;HI><B28A@F4C;:G79D563=1?E8?7;=CD93B215A@H:EI4<>6FG31?@2>9CA8=DFE4:57BGH;I<6FGHDC456<1?B@;3I>28=9:E7AI4A8<=@7B2>9:56?HF;EDGC13=>5E;FH:D37GI1CA496<28?@B7:9B6GI?E;H8A2<CD13@=4>5F4;25>9=16E<:7GA3C?DHFB@I89=:7FB3>2C5?D@148I<AG6;EH?C@IH84DGA3;E62B1:F>59<=7B36G1<:F7@8IH=>2;5E9?CAD4<8DAE5?I;H94CBF=6@G7>1:325EI13AC@8=:64<;9?H2FBD7G>>6;=@3B2?9D51F8GE47CAIH:<CB427D1H:GA>93E5I<=;8?F6@D9<?A7E4F>GH2I=8@6:BC35;1GHF:8I6;5<C@?7BD3>A14E92=";
            UpdateConstants((int)Math.Sqrt(Math.Sqrt(unsolvedBoard.Length)));
            int[] unsolvedBoardAsArray = InputUtils.InputParser(unsolvedBoard);
            var solver = new SodokuSolver(unsolvedBoardAsArray);

            // ACT
            solver.SolveSodoku();
            string result = solver.ReturnBoardAsString();

            // ASSERT
            Assert.AreEqual(solvedBoard, result);
        }

        /// <summary>
        /// 25x25 easy board
        /// </summary>
        [TestMethod]
        public void TwentyFiveOnTwentyFiveEasyTest()
        {
            // ARRANGE
            string unsolvedBoard = "0E487:009200I300000=<;0?0090:50>00G=1B00;60A<87FE003000=1BC00;0?070008:5@9200D=1<00?080FE450920000006?A0;80FE400092>I0G0010C0E48705I000003G00000100?0<90:0I>B30H10C00F00<;00E4000H>B1000=0000<@E4075I92:CD=060F?A07@E40I00:50B30H00000000000I02:B0GH>10000D=00000A00@94070000I00GH>A00FE00487030:5C0H00600=000009030:00C000?0006000<;2:5I0B000>6?0=1EA<;0@9000G0>B060D01FEA<;94870002:0:5000CD00B?A=004<0F092870H0BC00A010040;02800930:50=1000000000200@0:0I3CDH>000FE40087030:000000C0A=108709230:0IC00>BA=06000<007090:GH5I0D0>B00160A08;F05I00H00>00A006080FE00:000>000=A000048;0E00002G000006?A000;FE2:7@905I3G000000FE402:0@90H003000CDA<100";
            string solvedBoard = "FE487:5@92H>I3G1BCD=<;6?A@92:5H>I3G=1BCD;6?A<87FE4I3GH>=1BCD<;6?A7FE48:5@92BCD=1<;6?A87FE45@92:H>I3G6?A<;87FE4:5@92>I3GH=1BCDE487@5I92:>B3GH6CD=1;F?A<92:5I>B3GH16CD=F?A<;7@E483GH>B16CD=;F?A<@E4875I92:CD=16;F?A<7@E48I92:5>B3GH?A<;F7@E485I92:B3GH>16CD=D=16?FEA<;@948732:5IBCGH>A<;FE@9487I32:5CGH>B6?D=1487@9I32:5BCGH>?D=16FEA<;2:5I3BCGH>6?D=1EA<;F@9487GH>BC6?D=1FEA<;9487@I32:5:5I3GCDH>B?A=164<;FE9287@H>BCD?A=16E4<;F287@93G:5I=16?AE4<;F9287@G:5I3CDH>B<;FE49287@3G:5IDH>BC?A=1687@923G:5ICDH>BA=16?E4<;F7@92:GH5I3D=>BC<16?A48;FE5I3GHD=>BCA<16?8;FE42:7@9>BCD=A<16?48;FE:7@92GH5I316?A<48;FE2:7@9H5I3GD=>BC;FE482:7@9GH5I3=>BCDA<16?";
            UpdateConstants((int)Math.Sqrt(Math.Sqrt(unsolvedBoard.Length)));
            int[] unsolvedBoardAsArray = InputUtils.InputParser(unsolvedBoard);
            var solver = new SodokuSolver(unsolvedBoardAsArray);

            // ACT
            solver.SolveSodoku();
            string result = solver.ReturnBoardAsString();

            // ASSERT
            Assert.AreEqual(solvedBoard, result);
        }

        /// <summary>
        /// 25x25 medium board
        /// </summary>
        [TestMethod]
        public void TwentyFiveOnTwentyFiveMediumTest()
        {
            // ARRANGE
            string unsolvedBoard = "0000C000F000000000000000000000000000000000000000000000030000000000000000000000000E00000000000000000000H000000000000450000000000000000000000000000000000000F0G000000000000;00:00000000000000000000000000A0000;000000900000000000000000000000000000000000G0000000000?0000000000000000207000000000000G0000000000000000000000?000000000000000000000000000000000000000000000@000000000000030000000000000000000000000000000000000000000000000000@000000000000000000000000>0000000000000000000000000000000<G000000000000000000000000000000000000600000?000000000000000000000000000000000000000000000000000000000009001>00000000000000000000000000000000H";
            string solvedBoard = "126DC=5>F498:@ABEGHI;<7?3F;GI@C?D78456BE31<>A:=2H9B=>E<3@G9:CDFHI78;?2546A1?45A8HEI;<1237G69:=F>@BCD93H:7A6B12<=>;?45@CDEF8GIHI8<D>B126=?GC3:AF74@5E9;@A=7F8G9CDB1E5>H6I<;?3:42CG:9EF3;45D6<I2?=81@B7H>A4523;:=?AHF9@87DBEG>C1I6<6?1B>@<7IEAH;4:5293CF8GD=5BI=G2>3?76FC9<@:1D8AH4;E2C7>34A:B18E5=;GH6F9<ID@?;<41H5D=E9I:B?@>7A236GC8F8D9@:6FCG;H3A>4E?BI<721=5A6EF?<I8H@2G7D1;4C5=9:>3BE9C64125>?;@H38<D7:BIA=FG3:;?B746DA><=29IFH8G1E@5C=@F5198E3B:IDG6AC>4?H;<27G><HI;:F@=7A4EC23561D9?B8D7A82ICH<G5B?1F9;=@E4>3:67F@2=?945CE>IAD8<3BHG6;1::H?C6B;<8>G41F5=I2973DAE@<13;ADH@:F?726=CG4E58B9I>I8BG5E7A=3@C9:H1>D;62?F<4>ED49G126I3;8<BF@?A:=C57H";
            UpdateConstants((int)Math.Sqrt(Math.Sqrt(unsolvedBoard.Length)));
            int[] unsolvedBoardAsArray = InputUtils.InputParser(unsolvedBoard);
            var solver = new SodokuSolver(unsolvedBoardAsArray);

            // ACT
            solver.SolveSodoku();
            string result = solver.ReturnBoardAsString();

            // ASSERT
            Assert.AreEqual(solvedBoard, result);
        }

    }
}
